var bn = document.querySelector("#btn_menu");
var content = document.querySelector(".content");
let sidebar = document.querySelector(".sidebar");
let searchBtn = document.querySelector(".bx-search");

bn.onclick = function () {
    sidebar.classList.toggle("sidebar_hide");
    if (bn.classList.contains("bx-menu")) {
        bn.classList.replace("bx-menu", "bx-menu-alt-right");
        content.classList.replace("content", "content_hide");
    } else {
        bn.classList.replace("bx-menu-alt-right", "bx-menu");
        content.classList.replace("content_hide", "content");
    }
}





var button1 = document.querySelector("#ActionButton1");
var button2 = document.querySelector("#ActionButton2");
var button3 = document.querySelector("#ActionButton3");
var button4 = document.querySelector("#ActionButton4");
var button5 = document.querySelector("#ActionButton5");

var tap1 = document.querySelector("#inicio");
var tap2 = document.querySelector("#productRegistrados");
var tap3 = document.querySelector("#totalSalidas");
var tap4 = document.querySelector("#totalEntradas");
var tap5 = document.querySelector("#productSolicitar");




button1.onclick = function () {
    tap1.classList = "contenedor";
    tap2.classList = "contenedor";
    tap3.classList = "contenedor";
    tap4.classList = "contenedor";
    tap5.classList = "contenedor";

    tap1.classList = "activo";
  


}


button2.onclick = function () {
    tap1.classList = "contenedor";
    tap2.classList = "contenedor";
    tap3.classList = "contenedor";
    tap4.classList = "contenedor";
    tap5.classList = "contenedor";

    tap2.classList = "activo";



}


button3.onclick = function () {
    tap1.classList = "contenedor";
    tap2.classList = "contenedor";
    tap3.classList = "contenedor";
    tap4.classList = "contenedor";
    tap5.classList = "contenedor";

    tap3.classList = "activo";



}

button4.onclick = function () {
    tap1.classList = "contenedor";
    tap2.classList = "contenedor";
    tap3.classList = "contenedor";
    tap4.classList = "contenedor";
    tap5.classList = "contenedor";

    tap4.classList = "activo";

}

button5.onclick = function () {
    tap1.classList = "contenedor";
    tap2.classList = "contenedor";
    tap3.classList = "contenedor";
    tap4.classList = "contenedor";
    tap5.classList = "contenedor";

    tap5.classList = "activo";



}

function CrearModal() {
  
   $('#create').modal('show');
}
function hidemodal(id) {

    $(id+'').modal('hide');
    document.getElementById("form").reset();
   
   
}
function delmodal() {
    var modal = $('.del');
    for (var i = 0; i < modal.length; i++) {
        modal[i].modal('hide');
    }
}


function mostrar(evt, tab) {
  
    var i, tabcontent, tablinks;

  
    tabcontent = document.getElementsByClassName("tab_content");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

 
    tablinks = document.getElementsByClassName("tab");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

  
    document.getElementById(tab).style.display = "block";
    evt.currentTarget.className += " active";
}

function goBack() {
    window.history.back();
}



