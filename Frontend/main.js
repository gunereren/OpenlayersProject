import Map from 'ol/Map.js';
import View from 'ol/View.js';
import { Draw, Modify, Snap } from 'ol/interaction.js';
import { OSM, Vector as VectorSource } from 'ol/source.js';
import { Tile as TileLayer, Vector as VectorLayer } from 'ol/layer.js';
import { get } from 'ol/proj.js';
import WKT from 'ol/format/WKT.js';

const saveParcelBtn = document.getElementById("saveParcel");
const mainEditBtn = document.getElementById("mainEditButton");
const cancelBtn = document.getElementById("cancelBtn");

const raster = new TileLayer({
    source: new OSM(),
});

var format = new WKT();

let source = new VectorSource();
const vector = new VectorLayer({
    source: source,
    style: {
        'fill-color': 'rgba(255, 255, 255, 0.2)',       // Seçim yaptığı alanın rengi.                                  Default: rgba(255, 255, 255, 0.2)
        'stroke-color': '#fa0000',                      // Seçim yaptığı alanın sınır çizgilerinin rengi.               Default: #ffcc33
        'stroke-width': 2,                              // Seçim yaptığı alanın sınır çizgilerinin kalınlığı.           Default: 2
        'circle-radius': 7,                             // Point type olunca haritada bıraktığı darielerin yarıçapı.    Default: 7
        'circle-fill-color': '#ffcc33',                 // Point type olunca haritada bıraktığı darielerin rengi.       Default: #ffcc33
    },
});

// Limit multi-world panning to one world east and west of the real world.
// Geometry coordinates have to be within that range.
const extent = get('EPSG:3857').getExtent().slice();
extent[0] += extent[0];
extent[2] += extent[2];
const map = new Map({
    layers: [raster, vector],
    target: 'map',
    view: new View({
        center: [0, 0],                     // Harita ilk açıldığında karşımıza çıkan konum
        zoom: 2,                                        // Harita ilk açıldığında zoom miktarı
        extent,
    }),
});

const modify = new Modify({ source: source });
map.addInteraction(modify);

let draw, snap; // global so we can remove them later
const typeSelect = document.getElementById('type');

function addInteractions() {
    draw = new Draw({
        source: source,
        type: typeSelect.value,
    });
    map.addInteraction(draw);
    snap = new Snap({ source: source });
    map.addInteraction(snap);
    draw.addEventListener("drawend", onDrawEnd);            // çizme işlemi bitince tetiklenecek

}

// PARSELİ KAYDET BUTONUNA BASINCA OLACAKLAR
saveParcelBtn.addEventListener("click", function () {
    var inputElements = document.getElementsByClassName("inputBox");
    var tablo = document.getElementById("table");
    var yeniSatir = tablo.insertRow(tablo.rows.length);
    yeniSatir.style = "background-color: white;"

    var huc1 = yeniSatir.insertCell(0);
    var huc2 = yeniSatir.insertCell(1);
    var huc3 = yeniSatir.insertCell(2);
    var huc4 = yeniSatir.insertCell(3);
    /*
        
        huc1.innerHTML = inputElements[0].value;
        huc2.innerHTML = inputElements[1].value;
        huc3.innerHTML = inputElements[2].value;
     */

    var duzenleButon = document.createElement("button");        // Edit butonu
    duzenleButon.innerHTML = '<i class="fa-regular fa-pen-to-square"></i> Edit';
    duzenleButon.style = "margin:0 1rem; text-align: center;"
    huc4.appendChild(duzenleButon);

    duzenleButon.onclick = function (event) {
        var closestElement = event.target.closest("tr");
        console.log(closestElement);
        debugger
        var id = duzenleButon.id;
        editingPopup(id);
    };

    var silButon = document.createElement("button");            // Delete butonu
    silButon.innerHTML = "<i class=\"fa-solid fa-xmark\" style=\"color: #000000;\"></i> Delete";
    huc4.appendChild(silButon);

    silButon.onclick = function () {
        var deleteId = silButon.id;
        deleteRow(deleteId);
    };

    for (var i = 0; i < inputElements.length; i++) {                    // Girilen değerleri okuyup inputBox'ı temizleyen döngü
        inputElements[i].value = "";
    }
    popup.style.display = 'none';
    popupBackground.style.display = "none";
});

// TABLODAN ELEMAN SİLEN FONKSİYON
function deleteRow(deleteBtnId) {
    var deleteBtn = document.getElementById(deleteBtnId);
    var uyar = confirm("Bu satırı silmek istediğinize emin misiniz?");
    const silinecek = document.getElementById("drawId0")

    if (uyar) {
        var currentRow = deleteBtn.parentNode.parentNode;
        currentRow.parentNode.removeChild(currentRow);
    }
}

// TABLODA OLAN EDİT BUTONUNA TIKLAYINCA ÇALIŞAN
function editingPopup(btnID) {
    const editingPopup = document.getElementById("editingPopup");
    const popupbackground = document.getElementById("popupBackground");
    editingPopup.style.display = "block";
    popupbackground.style.display = "block";

    var editBtn = document.getElementById(btnID);
    editWithPopup(editBtn)


    const closeBtn = document.getElementById("editingClosePopupButton");
    closeBtn.onclick = editingPopupClose;
};

function editWithPopup(editBtn) {
    cancelBtn.onclick = editingPopupClose;
    var currentPopup = editBtn.parentNode.parentNode;
    var hucreler = currentPopup.getElementsByTagName('td');

    for (var i = 0; i < hucreler.length - 1; i++) {
        // edit popup'ında inputBox'ları dolduran döngü
        var editInputID = "editInput" + (i + 1);
        var inputBox = document.getElementById(editInputID);
        inputBox.value = hucreler[i].textContent;
    }
}

// DÜZENLEME POPUP KAPATMA BUTONU
function editingPopupClose() {
    const editingPopup = document.getElementById("editingPopup");
    editingPopup.style.display = "none";

    const popupBackground = document.getElementById("popupBackground");
    popupBackground.style.display = "none";
}

function veriiOkuBakim() {
    alert("Burayı düzenlemek lazım. Örnek bir veri çekme operasyonu");
    const denemeTxt = $.ajax({
        url: "https://localhost:44384/api/parcel/getall",
        method: "get",
        success: function (data) {
            console.log(data, "verisi okunduuu");
        },
        error: function () {
            console.log("okuyamadık hata oldu");
        }
    });

}

// POPUP KAPATMA BUTONU
const closePopupButton = document.getElementById('closePopupButton');
closePopupButton.addEventListener('click', () => {

    popup.style.display = 'none';
    const popupBackground = document.getElementById("popupBackground");
    popupBackground.style.display = "none";

    var cizimler = source.getFeatures();
    source.removeFeature(cizimler[cizimler.length - 1]);
});

// ZOOM BUTON KONTROLLERI
document.getElementById("zoom-out").addEventListener("click", function () {
    const view = map.getView();
    const zoom = view.getZoom();
    view.setZoom(zoom - 1);
});
document.getElementById("zoom-in").addEventListener("click", function () {
    const view = map.getView();
    const zoom = view.getZoom();
    view.setZoom(zoom + 1);
});

// ÇİZİM BİTİNCE ÇALIŞAN FONKSİYON
function onDrawEnd(event) {
    const popup = document.getElementById("popup");
    const popupbackground = document.getElementById("popupBackground");
    popup.style.display = "block";
    popupbackground.style.display = "block";

    var wkt = format.writeGeometry(event.feature.getGeometry());    // Geometriyi al ve writeGeometry fonksiyonunun içine at
    var transformedCoordinates = format.readGeometry(wkt).transform('EPSG:3857', 'EPSG:4326');
    console.log("WKT Geoms:", transformedCoordinates.flatCoordinates);
}

// ANA EKRANDA DURAN BÜYÜK EDİT BUTONU
mainEditBtn.addEventListener("click", veriiOkuBakim);

// Handle change event.
typeSelect.onchange = function () {
    map.removeInteraction(draw);
    map.removeInteraction(snap);
    addInteractions();
};

addInteractions();
