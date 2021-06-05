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
