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
    var tblenght = clone.childNodes[3].childNodes[1].childNodes[3].children.length-1;
    for (var i = tblenght; i > 0; i--)
    {
        var tr = clone.childNodes[3].childNodes[1].childNodes[3].children[i];
        tr.remove();
      
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
function AddPika(result) {
    //class PikaRrugaPagesa {
    //    constructor() {
    //        this.PikaRrugaPagesaId = 0;
    //        this.CurrencyId = 0;
    //        this.Currency = null;
    //        this.PikaRrugaId = 0;
    //        this.Pika = null;
    //        this.Pagesa = 0;
    //        this.ShpenzimXhiro = false;
    //        this.PagesaKryer = false;
    //        this.CreatedDate = new Date();
    //        this.UpdatedDate = new Date();
    //    }
    //}

    //class PikaRruga {
    //    PikaRrugaPagesa: PikaRrugaPagesa
    //    constructor() {
    //        this.PikaRrugaId = 0;
    //        this.PikaShkarkimiId = 0;
    //        this.PikaShkarkimi = null;
    //        this.RrugaId = 0;
    //        this.Rruga = null;
    //        this.PikaRrugaPagesa  = new [];
    //        this.CreatedDate = new Date();
    //        this.UpdatedDate = new Date();
    //        this.PikaShkarkimiName = "";
    //    }
    //}

 

    var row = document.getElementById("PikaRrugadetailRow0"); // find row to copy
    var table = document.getElementById("PikaRrugaTable");
    var body = document.getElementById("PikaRrugadetailBody"); // find table to append to
    var clone = row.cloneNode(true); // copy children too
    var count = $('#ShoferRrugadetailBody tr').length;
   // var countVM = $('#PikaRrugaPagesaTable tr').length;
    var ShoferRrugadetailBody = $('#PikaRrugadetailBody');
    var length = ShoferRrugadetailBody[0].children.length;
    debugger
    if (clone.childNodes[1].childNodes[1].value == "Zgjidh") {

        row.childNodes[1].childNodes[1].value = result.pikaShkarkimiName; //ajax  PikaRrugas[i].PikaShkarkimi?.Emri
        //check if ajx has more than one pages
        if (result.pikaRrugaPagesa.length > 1) {
            //   var tblenghtchildren = clone.childNodes[3].childNodes[1].childNodes[3].children.length;
            //   var tblenghtchildNodes = clone.childNodes[3].childNodes[1].childNodes[3].childNodes.length;

            // get body of pagesa table
            var PikaRrugaPagesaBody0 = document.getElementById("PikaRrugaPagesaBody0");
            //clone tr of pagesa
           // var tr = row.childNodes[3].childNodes[1].childNodes[3].childNodes[1].cloneNode(true);
            for (var i = 0; i < result.pikaRrugaPagesa.length-1 ; i++) {
                //add tr for each pika rruga pagesa
                var tr = row.childNodes[3].childNodes[1].childNodes[3].childNodes[1].cloneNode(true);
                PikaRrugaPagesaBody0.appendChild(tr)

            }
        }

        for (var Pagesacount = 0; Pagesacount < result.pikaRrugaPagesa.length; Pagesacount++) {
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[1].childNodes[1].value = result.pikaRrugaPagesa[Pagesacount].pagesa; //ajax
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[3].childNodes[1].value = result.pikaRrugaPagesa[Pagesacount].currencyId; //ajax
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].value = result.pikaRrugaPagesa[Pagesacount].pagesaKryer; //ajax
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].checked = result.pikaRrugaPagesa[Pagesacount].pagesaKryer

            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[1].childNodes[1].attributes.name.nodeValue = "PikaRrugas[0].PikaRrugaPagesa[" + Pagesacount  + "].Pagesa";
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[3].childNodes[1].attributes.name.nodeValue = "PikaRrugas[0].PikaRrugaPagesa[" + Pagesacount + "].CurrencyId";
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].attributes.name.nodeValue = "PikaRrugas[0].PikaRrugaPagesa[" + Pagesacount + "].PagesaKryer";
            // remove child row
            row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[7].childNodes[1].id = Pagesacount.toString();
            //add  child row
            row.childNodes[3].childNodes[1].childNodes[5].childNodes[1].childNodes[1].childNodes[1].id = "0"; //length.toString();
            //if (result.pikaRrugaPagesa[Pagesacount].pagesaKryer) {
            //    debugger
            //    row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].checked = 'checked'; //ajax
            //} else {
            //    debugger
            //    row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].checked = 'checked'; //ajax

            //}
        }
        

    } else { 
        // add ajax vlaues to a clone row and ad pika rruga pagesa rows to

        debugger
        clone.childNodes[1].childNodes[1].value = result.pikaShkarkimiName;
        clone.childNodes[1].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + length + "].PikaShkarkimiName";
        //romevoe child of clone 
        var tblenght = clone.childNodes[3].childNodes[1].childNodes[3].children.length;
        for (var i = tblenght-1; i>0  ; i--) {
            var tr = clone.childNodes[3].childNodes[1].childNodes[3].children[i];
            tr.remove();
        }


        if (result.pikaRrugaPagesa.length > 1) {


            debugger
           // var PikaRrugaPagesaBody0 = document.getElementById("PikaRrugaPagesaBody0");
            var PikaRrugaPagesaBody0 = clone.childNodes[3].childNodes[1].childNodes[3];
            for (var i = 0; i < result.pikaRrugaPagesa.length - 1; i++) {
                //add tr for each pika rruga pagesa
                var tr = clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].cloneNode(true);
                PikaRrugaPagesaBody0.appendChild(tr)

            }
        }
            //   var tblenghtchildren = clone.childNodes[3].childNodes[1].childNodes[3].children.length;
            //   var tblenghtchildNodes = clone.childNodes[3].childNodes[1].childNodes[3].childNodes.length;

            // get body of pagesa table
            //clone tr of pagesa
            // var tr = row.childNodes[3].childNodes[1].childNodes[3].childNodes[1].cloneNode(true);


          
            //add new child 
           // add values to new child
        for (var Pagesacount = 0; Pagesacount < result.pikaRrugaPagesa.length; Pagesacount++) {
            debugger
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[1].childNodes[1].value = result.pikaRrugaPagesa[Pagesacount].pagesa; //ajax
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[3].childNodes[1].value = result.pikaRrugaPagesa[Pagesacount].currencyId; //ajax
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].value = result.pikaRrugaPagesa[Pagesacount].pagesaKryer; //ajax
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].checked = result.pikaRrugaPagesa[Pagesacount].pagesaKryer

            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[1].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" +length+"].PikaRrugaPagesa[" + Pagesacount + "].Pagesa";
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[3].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" +length+"].PikaRrugaPagesa[" + Pagesacount + "].CurrencyId";
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" +length+"].PikaRrugaPagesa[" + Pagesacount + "].PagesaKryer";

            // remove child row
            clone.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[7].childNodes[1].id = Pagesacount.toString();
            //add  child row
            clone.childNodes[3].childNodes[1].childNodes[5].childNodes[1].childNodes[1].childNodes[1].id = length.toString();
            //if (result.pikaRrugaPagesa[Pagesacount].pagesaKryer) {
            //    debugger
            //    row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].checked = 'checked'; //ajax
            //} else {
            //    debugger
            //    row.childNodes[3].childNodes[1].childNodes[3].children[Pagesacount].childNodes[5].childNodes[1].checked = 'checked'; //ajax

            //}
        }
        // // Remove Row
        clone.childNodes[5].childNodes[1].id = length.toString();
           clone.childNodes[3].childNodes[1].id = "PikaRrugaPagesaTable" + length.toString();
   clone.childNodes[3].childNodes[1].childNodes[3].id = "PikaRrugaPagesaBody" + length.toString();
   clone.id = "PikaRrugadetailRow" + length.toString(); // change id or other attributes/contents
        body.appendChild(clone);





   // // Remove Row
    clone.childNodes[5].childNodes[1].id = length.toString();

   // var countVM = 0; //pikarrugapagesa foreach nga ajax

   // // remove child row
   // clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[7].childNodes[1].id = countVM.toString();
   // //add  child row
   // clone.childNodes[3].childNodes[1].childNodes[5].childNodes[1].childNodes[1].childNodes[1].id = length.toString();

   // clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[1].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + length + "].PikaRrugaPagesa[" + countVM + "].Pagesa";
   //// clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[1].childNodes[1].value = "PikaRrugas[" + length + "].PikaRrugaPagesa[" + countVM + "].Pagesa"; //ajax
   // clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[3].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + length + "].PikaRrugaPagesa[" + countVM + "].CurrencyId";
   //// clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[3].childNodes[1].value = "PikaRrugas[" + length + "].PikaRrugaPagesa[" + countVM + "].CurrencyId"; //ajax
   // clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[5].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + length + "].PikaRrugaPagesa[" + countVM + "].PagesaKryer";
   //// clone.childNodes[3].childNodes[1].childNodes[3].childNodes[1].childNodes[5].childNodes[1].value = "PikaRrugas[" + length + "].PikaRrugaPagesa[" + countVM + "].PagesaKryer"; //ajax
   // clone.childNodes[3].childNodes[1].id = "PikaRrugaPagesaTable" + length.toString();
   // clone.childNodes[3].childNodes[1].childNodes[3].id = "PikaRrugaPagesaBody" + length.toString();
   // clone.id = "PikaRrugadetailRow" + length.toString(); // change id or other attributes/contents

    //var tblenght = clone.childNodes[3].childNodes[1].childNodes[3].children.length;
    //for (var i = 3; i <= (tblenght * 2) - 1; i++) {
    //    var tr = clone.childNodes[3].childNodes[1].childNodes[3].childNodes[i];
    //    tr.remove();
    //    i++;
    //        }

    // clone.childNodes[3].childNodes[1].childNodes[3].
    //    body.appendChild(clone);



    }
    // add new row to end of table
}
function AddrowChild(e) {
    debugger

    var row = document.getElementById("detailRow"); // find row to copy
    var table = document.getElementById("ShoferRrugaTable");
    //var body = document.getElementById("StudentsTablebody" + e.toString()); // find table to append to
    var body = e.offsetParent.offsetParent.children[1];
    var clone = row.cloneNode(true); // copy children too
    var count = $('#ShoferRrugadetailBody tr').length;
    var ShoferRrugadetailBody = $('#ShoferRrugadetailBody');
    var length = ShoferRrugadetailBody[0].children.length - 1;
    //var countVM = $('#StudentsTableRow tr').length;
    var countVM = body.children.length;
    // clone.childNodes[1].childNodes[1].attributes.name.nodeValue = "ShoferitRrugaVM[" + ((count / 4) - (countVM * 2)) + "].ShoferId";
    clone.childNodes[1].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + e.id + "].PikaRrugaPagesa[" + (countVM) + "].Pagesa";

    clone.childNodes[3].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + e.id + "].PikaRrugaPagesa[" + (countVM) + "].CurrencyId";
    clone.childNodes[5].childNodes[1].attributes.name.nodeValue = "PikaRrugas[" + e.id + "].PikaRrugaPagesa[" + (countVM) + "].PagesaKryer";
    clone.childNodes[7].childNodes[1].id = (countVM).toString();

    clone.id = "detailRow" + countVM.toString(); // change id or other attributes/contents
    body.appendChild(clone); // add new row to end of table
}

function RemovePikaRow(e) {
    debugger

    var trNumber = e.offsetParent.offsetParent.childNodes[3].children.length - 1;
    if (e.id == 0 && trNumber == 0) {
        e.offsetParent.offsetParent.childNodes[3].children[0].children[0].children[0].value = "Zgjidh"
        //tpinnertablebody  tr count
        var tblenght = e.offsetParent.offsetParent.childNodes[3].children[0].children[1].children[0].children[1].children.length;
        for (var i = tblenght - 1; i > 0; i--) {
            var tr = e.offsetParent.offsetParent.childNodes[3].children[0].children[1].children[0].children[1].children[i];
            tr.remove();
        }
        return
    }
    if (e.id != trNumber)
        return
    // var tr = e.offsetParent;
    // var tr = document.getElementById("ShoferRrugadetailRow" + e.id.toString())
    var trlength = e.offsetParent.offsetParent.childNodes[3].children.length - 1;
    var tr = e.offsetParent.offsetParent.childNodes[3].children[trlength];
    tr.remove();
}
