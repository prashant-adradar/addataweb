(function () {

    $(function () {
        highlightSortByHeader();
    });

    function highlightSortByHeader() {
        var vhref = $(location).attr("href");
        var vhost = $(location).attr("host");
        var vprotocol = $(location).attr("protocol");
        var vserver = vprotocol + "//" + vhost + "/";
        var vuri = vhref.replace(vserver, "").replace("AdradarAdDataWeb/", "");
        //alert("uri: " + vuri);

        var vpagetype;
        var arruri;
        var vsortby;
        if (vuri.length > 0) {
            arruri = vuri.split("/");
            if (arruri.length > 2) {
                vpagetype = arruri[1];
                vsortby = arruri[3];
            } else if (arruri.length > 1) {
                vpagetype = arruri[1];
                if (vpagetype == "Index") {
                    vsortby = "brandname";
                } else if (vpagetype == "List2") {
                    vsortby = "brandname";
                }
            } else {
                vpagetype = "Index";
                vsortby = "brandname";
            }
        } else {
            vpagetype = "Index";
            vsortby = "brandname";
        }
        if ("adid,brandid,brandname,numpages,position".indexOf(vsortby) == -1) return;
        //alert(vpagetype + ": " + vsortby);

        var vlookfor_header_text = "";
        switch (vsortby) {
            case "adid":
                vlookfor_header_text = __Ad_AdId_DisplayName;
                break;
            case "brandid":
                vlookfor_header_text = __Brand_BrandId_DisplayName;
                break;
            case "brandname":
                vlookfor_header_text = __Brand_BrandName_DisplayName;
                break;
            case "numpages":
                vlookfor_header_text = __Ad_NumPages_DisplayName;
                break;
            case "position":
                vlookfor_header_text = __Ad_Position_DisplayName;
                break;
        }
        var vcolumnheaders = $.each($("#addatatable tr th"), function (index, cellelement) {
            var vcell = $(cellelement);
            //alert(vcell.text());
            if (vcell.text().indexOf(vlookfor_header_text) != -1) {
                vcell.addClass("label-warning");
            }
        });
    }

})();
