/// <reference path="../../sheetjs/xlsx.full.min.js" />
var Al_Data = "";

function Data() {

    
    //JQuery开始

    $(function () {

        //获取输入框内容
        var minlong = $("#minlong").val();
        var maxlong = $("#maxlong ").val();
        var minlat = $("#minlat").val();
        var maxlat = $("#maxlat").val();

        var minlevel = $("#minlevel").val();
        var maxlevel = $("#maxlevel").val();


        //console.log(minlong, maxlong, minlat, maxlat);
        //alert(check(minlong, maxlong, minlat, maxlat));


        //只要不为空就判断是否符合条件 如果为空就直接过
        //if ((!minlong.replace(/\s*/g, "") == "" && !checkLong(minlong))
        //    || (!maxlong.replace(/\s*/g, "") == "" && !checkLong(maxlong))
        //    || (!minlat.replace(/\s*/g, "") == "" && !checkLat(minlat))
        //    || (!maxlat.replace(/\s*/g, "") == "" && !checkLat(maxlat))
        //    )





        if (check(minlong, maxlong, minlat, maxlat, minlevel, maxlevel) != 6)
        {//如果去掉空格不为空
            alert('可以不输入，如果输入，请输入正确的经纬度范围，精度范围:-180~180 纬度，范围: -90~90  小数部分为0到6位 震级请输入整数或小数');
            //alert(checkLong(minlong));
        
            return;
        }




        //为空或者值 符合条件存在
        else {
            //alert('请输入正确的经纬度范围，精度范围:-180~180 纬度，范围: -90~90');

            //获取表单数据 初始序列化
            var data = encodeURI(jQuery("#frm").serialize());

            //alert(data);
            //异步发送数据
            $.ajax({
                type: 'POST',
                url: "Data",
                data: data,
                dataType: "json",   //服务器响应的数据类型
                success: function (data) {
                    Al_Data = data;//   全局变量复制
                    //处理json数据
                   // console.log(data);
                    $dataJson = $.parseJSON(data);

                    //如果是一个数据 alert($dataJson["CityId"]);
                   // alert($dataJson["end"]);
                    //获取数据个数
                    //$dataNum = data.length;  jsonjson字符串的个数

                    //获取数据个数


                    $dataNum = $dataJson.length;
                    //alert("个数为" + $dataNum);





                    //设置table tr
                    $html = '<table border="0" cellpadding="0" cellspacing="0" class="speed-table1">' +
                        '<tr class="speed-tr-h1">' +
                        '<th width="70" align="center" style="padding-left: 20px">强烈程度</th>' +
                        '<th width="126" align="center">发生时刻(UTC+8)</th>' +

                        '<th width="60" align="center">纬度(°)</th>' +
                        '<th width="60" align="center">经度(°)</th>' +
                        '<th width="79" align="center">影响范围(千米)</th>' +
                        '<th width="260" align="center">参考位置</th>' +
                        '<!--                                <th width="85">操作</th>-->' +
                        '</tr>',





                        //将数据填充到表

                        $j = 0;
                    for ($i = 0; $i < $dataNum; $i++) $j++,
                        $j % 2 == 0 ? $dataJson[$i].Elevel >= 6 ? $html += '<tr class="tr-red" style="background-color: #eeeeee;">' : $dataJson[$i].Elevel >= 4 ? $html += '<tr style="background-color: #eeeeee;font-weight: bold;">' : $html += '<tr style="background-color: #eeeeee;">' : $dataJson[$i].CityId >= 6 ? $html += '<tr class="tr-red">' : $dataJson[$i].CityId ? $html += '<tr style="font-weight: bold;">' : $html += "<tr>",


                        $html += '<td align="center" style="padding-left: 20px">' + $dataJson[$i].Elevel + '</td><td align="center">' + $dataJson[$i].ETime + '</td><td align="center">' + $dataJson[$i].ELat + '</td><td align="center">' + $dataJson[$i].ELong + "</td>",


                        $html += '<td align="center">' + $dataJson[$i].Elevel + '</td><td align="left"><a href="/Map/Detail?location=' + $dataJson[$i].ELocation + '&&type='+$dataJson[$i].TId+'&&date=' + $dataJson[$i].ETime + '" ' + ' target="_blank"' + '>' + $dataJson[$i].ELocation + "</a></td></tr>";
                        $html += "</table>",




                    //填充table
                        $("#speedquery").html($html)

                    $("#pagenum").text("当前查询条数:  " + $dataNum);

                },
                error: function () {
                    alert("服务器繁忙请稍后");
                }

            })

        }


    });


}




//校验经度
function checkLong(lat) {
    var longrg = /^(\-|\+)?(((\d|[1-9]\d|1[0-7]\d|0{1,3})\.\d{0,6})|(\d|[1-9]\d|1[0-7]\d|0{1,3})|180\.0{0,6}|180)$/;
    //var lng = $("#itemform [id='lng']").val();


    if (!longrg.test(lat)) {
        return '经度整数部分为0-180,小数部分为0到6位!'; 
    }
    return true;
}

//校验纬度是否符合规范
function checkLat(long) {
    var latreg = /^(\-|\+)?([0-8]?\d{1}\.\d{0,6}|90\.0{0,6}|[0-8]?\d{1}|90)$/;
    if (!latreg.test(long)) {
       return '纬度整数部分为0-90,小数部分为0到6位!';
    }
     return true;
}





function check(minlong, maxlong, minlat, maxlat,minlevel,maxlevel) {
    var add = 0;
    /*判断minlong*/
    if (!minlong.replace(/\s*/g, "")=="")   //如果不为空
    {
        if (checkLong(minlong) == true) { add++; }
        //if (checkLong(minlong) == true) { console.log(minlong); add++; }
    }
    else
    {
        add++;
    }


    /*判断minlat*/
    if (!minlat.replace(/\s*/g, "") == "")   //如果不为空
    {
        //fuhecheck条件
        if (checkLat(minlat) == true) { add++; }
        //if (checkLat(minlat) == true) { console.log(minlat);add++; }
    }
    else {
        add++;
    }



    /*判断maxlong*/

    if (!maxlong.replace(/\s*/g, "") == "")   //如果不为空
    {
        //fuhecheck条件
        //if (checkLong(maxlong) == true) { console.log(maxlong);add++; }

        if (checkLong(maxlong) == true) {add++; }
    }
    else {
        add++;
    }




    /*maxlat*/
    if (!maxlat.replace(/\s*/g, "") == "")   //如果不为空
    {
        //fuhecheck条件
        if (checkLat(maxlat) == true) { add++; }
        //if (checkLat(maxlat) == true) { console.log(maxlat);add++; }
    }
    else {
        add++;
    }


    var pattern = /^([1-9]\d*|0)(\.\d{1,2})?$/;
    if (!minlevel.replace(/\s*/g, "") == "")   //如果不为空
    {
        if (pattern.test(minlevel)) { add++; }
    }
    else {
       
        add++;
    }
    if (!maxlevel.replace(/\s*/g, "") == "")   //如果不为空
    {
        if (pattern.test(maxlevel)) { add++; }
    }
    else {

        add++;
    }


    return add;
}




function selectFile() {
	document.getElementById('file').click();
}

// 读取本地excel文件
function readWorkbookFromLocalFile(file, callback) {
	var reader = new FileReader();
	reader.onload = function (e) {
		var data = e.target.result;
		var workbook = XLSX.read(data, { type: 'binary' });
		if (callback) callback(workbook);
	};
	reader.readAsBinaryString(file);
}

// 从网络上读取某个excel文件，url必须同域，否则报错
function readWorkbookFromRemoteFile(url, callback) {
	var xhr = new XMLHttpRequest();
	xhr.open('get', url, true);
	xhr.responseType = 'arraybuffer';
	xhr.onload = function (e) {
		if (xhr.status == 200) {
			var data = new Uint8Array(xhr.response)
			var workbook = XLSX.read(data, { type: 'array' });
			if (callback) callback(workbook);
		}
	};
	xhr.send();
}

// 读取 excel文件
function outputWorkbook(workbook) {
	var sheetNames = workbook.SheetNames; // 工作表名称集合
	sheetNames.forEach(name => {
		var worksheet = workbook.Sheets[name]; // 只能通过工作表名称来获取指定工作表
		for (var key in worksheet) {
			// v是读取单元格的原始值
			console.log(key, key[0] === '!' ? worksheet[key] : worksheet[key].v);
		}
	});
}

function readWorkbook(workbook) {
	var sheetNames = workbook.SheetNames; // 工作表名称集合
	var worksheet = workbook.Sheets[sheetNames[0]]; // 这里我们只读取第一张sheet
	var csv = XLSX.utils.sheet_to_csv(worksheet);
	document.getElementById('result').innerHTML = csv2table(csv);
}






////////////////////////////////// 下载


// 将一个sheet转成最终的excel文件的blob对象，然后利用URL.createObjectURL下载
function sheet2blob(sheet, sheetName) {
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

function exportExcel() {
	var csv = table2csv($('#result table')[0]);
	var sheet = csv2sheet(csv);
	var blob = sheet2blob(sheet);
	openDownloadDialog(blob, '导出.xlsx');
}

function exportSpecialExcel() {

    
    aoa = Al_Data;//获取返回数据 

    //操作
    ////console.log(aoa); //
    ////console.log(typeof (aoa)); //这时候是字符串
    aoa = JSON.parse(aoa);   //转化
    //console.log(typeof (aoa));
    //console.log(aoa);
    var EventType;
 
    for (var i = 0; i < aoa.length; i++) {

        //console.log(aoa[i].Type.TName); //提取出名称
        aoa[i].EventType = aoa[i].Type.TName;
        // 删除多于/
        
        delete aoa[i].Type;
        delete aoa[i].EId;
        delete aoa[i].TId;
        //aoa[i].EventType =
    }
    //去除Type属性
    //console.log(aoa);
    

    var sheet = XLSX.utils.json_to_sheet(aoa);
    /////  console.log(sheet);  //You need to parse it first with JSON.parse:
    var time = new Date();
    var NowTime = time.getFullYear() + "" + time.getMonth() + "" + time.getDate() + "" + time.getHours() + "" + time.getMinutes() + time.getSeconds();
    openDownloadDialog(sheet2blob(sheet),NowTime+ '数据下载.xlsx');    


}