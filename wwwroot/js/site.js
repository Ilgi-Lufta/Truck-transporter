// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function myFunction() {
//    var element = document.getElementById("myDIV");
//    if(element.classList.contains("none")){
//        element.classList.remove("none");

//    }else{

//        element.classList.add("none");
//    }
//}

//function myFunction() {
//    debugger
//    var row = document.getElementById("ShoferRrugadetailRow"); // find row to copy
//    var table = document.getElementById("ShoferRrugaTable"); // find table to append to
//    var body = document.getElementById("ShoferRrugadetailBody"); // find table to append to
//    var row = table.ariaRowCount;
//    var x = document.getElementById("ShoferRrugaTable").rows.length;
//    var count = $('#ShoferRrugaTable tr').length;
//     var test =row.cloneNode(false);
//    var clone = row.cloneNode(true); // copy children too
//    clone.id = "newID"; // change id or other attributes/contents
//    body.appendChild(clone); // add new row to end of table
//}

function myFunction() {
    debugger
    var row = document.getElementById("ShoferRrugadetailRow0"); // find row to copy
    var table = document.getElementById("ShoferRrugaTable");
    var body = document.getElementById("ShoferRrugadetailBody"); // find table to append to
    var clone = row.cloneNode(true); // copy children too
    var count = $('#ShoferRrugadetailBody tr').length;
    var countVM = $('#StudentsTableRow tr').length;
    var ShoferRrugadetailBody = $('#ShoferRrugadetailBody');
    var length = ShoferRrugadetailBody[0].children.length ;
    clone.childNodes[1].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + length + "].ShoferId";
    // Remove Row
    clone.childNodes[5].childNodes[1].id = length.toString();

    // remove child row
    clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[7].childNodes[1].id = countVM.toString();
    //add  child row
    clone.childNodes[3].childNodes[1].childNodes[5].childNodes[1].childNodes[1].childNodes[1].id = length.toString();

    clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[1].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + length + "].PagesaShoferitVM[" + countVM + "].Pagesa";
    clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[3].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + length + "].PagesaShoferitVM[" + countVM + "].CurrencyId";
    clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[5].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + length + "].PagesaShoferitVM[" + countVM + "].PagesaKryer";
    clone.childNodes[3].childNodes[1].id = "StudentsTable" + length.toString();
    clone.childNodes[3].childNodes[1].childNodes[3].id = "StudentsTablebody" + length.toString();
    clone.id = "ShoferRrugadetailRow" + length.toString(); // change id or other attributes/contents
    var tblenght = clone.childNodes[3].childNodes[1].childNodes[3].children.length;
    for (var i = 3; i <= (tblenght*2) - 1; i++)
    {
        var tr = clone.childNodes[3].childNodes[1].childNodes[3].childNodes[i];
        tr.remove();
        i++;
    }
   // clone.childNodes[3].childNodes[1].childNodes[3].
    body.appendChild(clone); // add new row to end of table
}
function RemoveRow(e) {
    debugger

    var trNumber = e.offsetParent.offsetParent.childNodes[3].children.length - 1;
    if (e.id == 0 || e.id != trNumber) 
        return
   // var tr = e.offsetParent;
   // var tr = document.getElementById("ShoferRrugadetailRow" + e.id.toString())
    var trlength = e.offsetParent.offsetParent.childNodes[3].children.length - 1;
    var tr = e.offsetParent.offsetParent.childNodes[3].children[trlength];
       tr.remove();
}
function RemoveChildRow(e) {
    debugger
    var trNumber = e.offsetParent.offsetParent.childNodes[3].children.length - 1;
    if (e.id == 0 || e.id != trNumber) 
        return
    var trlength = e.offsetParent.offsetParent.childNodes[3].children.length - 1;
    var tr = e.offsetParent.offsetParent.childNodes[3].children[trlength];
   // var tr = document.getElementById("detailRow" + e.id.toString())
    tr.remove();
}
function myFunctionChild(e) {
    debugger

    var row = document.getElementById("detailRow"); // find row to copy
    var table = document.getElementById("ShoferRrugaTable");
    var body = document.getElementById("StudentsTablebody"+e.toString()); // find table to append to
    var clone = row.cloneNode(true); // copy children too
    var count = $('#ShoferRrugadetailBody tr').length;
    var ShoferRrugadetailBody = $('#ShoferRrugadetailBody');
    var length = ShoferRrugadetailBody[0].children.length-1;
    //var countVM = $('#StudentsTableRow tr').length;
    var countVM =body.children.length;
   // clone.childNodes[1].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + ((count / 4) - (countVM * 2)) + "].ShoferId";
    clone.childNodes[1].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + e + "].PagesaShoferitVM[" + (countVM) + "].Pagesa";

    clone.childNodes[3].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + e + "].PagesaShoferitVM[" + (countVM ) + "].CurrencyId";
    clone.childNodes[5].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + e + "].PagesaShoferitVM[" + (countVM) + "].PagesaKryer";
    clone.childNodes[7].childNodes[1].id = (countVM ).toString();
    
    clone.id = "detailRow" + countVM.toString(); // change id or other attributes/contents
    body.appendChild(clone); // add new row to end of table
}
