/// <reference path="../sheetjs/xlsx.core.min.js" />
/*
 *
 * Editor：赵传旭
 * Adim:后台界面导出数据
 * 注意使用回调函数使用DownLoadData返回Json下载
 * */


////var a = function () {
////    new_element = document.createElement("script");
////    new_element.setAttribute("type", "text/javascript");
////    new_element.setAttribute("src","../sheetjs/xlsx.core.min.js");  //引进Jquery 其他
////    document.body.appendChild(new_element);


////}//匿 名


function adminExcel(type) {
//对应类型 0：User 1：Investigator2 ：event3：Unit
    ///alert(type);

    $.ajax({
        type: 'GET',   //方式
        url: '/Admin/DownLoadAdminData?type='+type, //地址
        ////data: type,
        dataType: "json",   //服务器响应的数据类型
        success: function (data) {  //传递过来的JSon
            data = JSON.parse(data);
            //  alert(data); console.log(data);

            exportSpecialExcel(data);
        }
        //error: alert("服务器繁忙，请稍后再试")
    });


} function sheet2blob(sheet, sheetName) {
    sheetName = sheetName || 'sheet1';
    var workbook = {
        SheetNames: [sheetName],
        Sheets: {}
    };
    workbook.Sheets[sheetName] = sheet;
    // 生成excel的配置项
    var wopts = {
        bookType: 'xlsx', // 要生成的文件类型
        bookSST: false, // 是否生成Shared String Table，官方解释是，如果开启生成速度会下降，但在低版本IOS设备上有更好的兼容性
        type: 'binary'
    };
    var wbout = XLSX.write(workbook, wopts);
    var blob = new Blob([s2ab(wbout)], { type: "application/octet-stream" });
    // 字符串转ArrayBuffer
    function s2ab(s) {
        var buf = new ArrayBuffer(s.length);
        var view = new Uint8Array(buf);
        for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
        return buf;
    }
    return blob;
}

/**
 * 通用的打开下载对话框方法，没有测试过具体兼容性
 * @param url 下载地址，也可以是一个blob对象，必选
 * @param saveName 保存文件名，可选
 */
function openDownloadDialog(url, saveName) {
    if (typeof url == 'object' && url instanceof Blob) {
        url = URL.createObjectURL(url); // 创建blob地址
    }
    var aLink = document.createElement('a');
    aLink.href = url;
    aLink.download = saveName || ''; // HTML5新增的属性，指定保存文件名，可以不要后缀，注意，file:///模式下不会生效
    var event;
    if (window.MouseEvent) event = new MouseEvent('click');
    else {
        event = document.createEvent('MouseEvents');
        event.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
    }
    aLink.dispatchEvent(event);
}



function loadRemoteFile(url) {
    readWorkbookFromRemoteFile(url, function (workbook) {
        readWorkbook(workbook);
    });
}

function exportSpecialExcel(aoa) {

    //alert(aoa);
   
   // aoa = JSON.parse(aoa);   //转化


    var sheet = XLSX.utils.json_to_sheet(aoa);
    /////  console.log(sheet);  //You need to parse it first with JSON.parse:
    var time = new Date();
    var NowTime = time.getFullYear() + "" + time.getMonth() + "" + time.getDate() + "" + time.getHours() + "" + time.getMinutes();
    openDownloadDialog(sheet2blob(sheet), NowTime + '数据下载.xlsx');


}