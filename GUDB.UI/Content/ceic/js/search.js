function search(e, t, n, r, i, s, o, u, a, f, l) {
    parseajax.abort(),
        parseajax = $.getJSON(site_url + "ajax/search?page=" + e + "&&start=" + t + "&&end=" + n + "&&jingdu1=" + r + "&&jingdu2=" + i + "&&weidu1=" + s + "&&weidu2=" + o + "&&height1=" + u + "&&height2=" + a + "&&zhenji1=" + f + "&&zhenji2=" + l + "&&callback=?",
            function (t) {
                var n = t.shuju;
                $html = '<table  border="0" cellpadding="0" cellspacing="0" class="speed-table1"><tr class="speed-tr-h1">',
                    $html += '<th width="51" align="center" style="padding-left: 20px">震级(M)</th><th width="126" align="center">发震时刻(UTC+8)</th><th width="60" align="center">纬度(°)</th>',
                    $html += '<th width="60" align="center">经度(°)</th><th width="79" align="center">深度(千米)</th><th width="260" align="center">参考位置</th></tr>',
                    len = n.length,
                    $j = 0;
                for ($i = 0; $i < n.length; $i++) $j++,
                    $j % 2 == 0 ? n[$i].M >= 6 ? $html += '<tr class="tr-red" style="background-color: #eeeeee;">' : n[$i].M >= 4 ? $html += '<tr style="background-color: #eeeeee;font-weight: bold;">' : $html += '<tr style="background-color: #eeeeee;">' : n[$i].M >= 6 ? $html += '<tr class="tr-red">' : n[$i].M >= 4 ? $html += '<tr style="font-weight: bold;">' : $html += "<tr>",
                    $html += '<td align="center" style="padding-left: 20px">' + n[$i].M + '</td><td align="center">' + n[$i].O_TIME + '</td><td align="center">' + n[$i].EPI_LAT + '</td><td align="center">' + n[$i].EPI_LON + "</td>",
                    $html += '<td align="center">' + n[$i].EPI_DEPTH + '</td><td align="left"><a href="' + static_url + n[$i].NEW_DID + '.html" cid="' + n[$i].CATA_ID + '" id="cid">' + n[$i].LOCATION_C + "</a></td></tr>";
                $html += "</table>",
                    $("#speedquery").html($html),
                    $("#paging").html(t.page),
                    $("#pagenum").html("当前页:" + e + "　总页数:" + t.num + "")
            },
            "jsonp")
}
var len = 0,
    parseajax;
window.ActiveXObject ? parseajax = new ActiveXObject("Microsoft.XMLHTTP") : window.XMLHttpRequest && (parseajax = new XMLHttpRequest),
    $(function () {
        $("#search").live("click",
            function () {
                var e = 1,
                    t = $("#start").val(),
                    n = $("#end").val(),
                    r = $("#jingdu1").val(),
                    i = $("#jingdu2").val(),
                    s = $("#weidu1").val(),
                    o = $("#weidu2").val(),
                    u = $("#height1").val(),
                    a = $("#height2").val(),
                    f = $("#zhenji1").val(),
                    l = $("#zhenji2").val();
                search(e, t, n, r, i, s, o, u, a, f, l)
            }),
            $(".page a").live("click",
                function () {
                    var e = $(this).attr("page"),
                        t = $("#start").val(),
                        n = $("#end").val(),
                        r = $("#jingdu1").val(),
                        i = $("#jingdu2").val(),
                        s = $("#weidu1").val(),
                        o = $("#weidu2").val(),
                        u = $("#height1").val(),
                        a = $("#height2").val(),
                        f = $("#zhenji1").val(),
                        l = $("#zhenji2").val();
                    search(e, t, n, r, i, s, o, u, a, f, l)
                }),
            $("#show").click(function () {
                if (len > 0) return $("#frm").attr("action", site_url + "onmap/id:0"),
                    $("#frm").submit(),
                    !1
            }),
            $("#save").click(function () {
                if (len > 0) return $("#frm").attr("action", site_url + "daochu/id:0"),
                    $("#frm").submit(),
                    !1
            })
    })