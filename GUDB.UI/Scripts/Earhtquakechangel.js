$(function () {

	//设置
	var limittext = $("#abc").
		limittext({
			"limit": 10, "fill": "......",
			"morefn": {
				"status": true, "moretext": "更多",
				"lesstext": "隐藏",
				cssclass: "limittextclass"//启用更多的A标签的CSS类名
				//"fullfn": function () { alert("more") },
				//"lessfn": function () { alert("less") }
			}
		});
	


	
	//

	//var limittextEDamageDes = $("#limittextEDamageDes").
	//	limittextEDamageDes({
	//		"limit": 10, "fill": "......",
	//		"morefn": {
	//			"status": true, "moretext": "更多",
	//			"lesstext": "隐藏部分",
	//			cssclass: "limittextclass"//启用更多的A标签的CSS类名
	//			//"fullfn": function () { alert("more") },
	//			//"lessfn": function () { alert("less") }
	//		}
	//	});、





	//设置地质灾害

	
	var limittext = $("#abcd").
		limittext({
			"limit": 10, "fill": "......",
			"morefn": {
				"status": true, "moretext": "更多",
				"lesstext": "隐藏",
				cssclass: "limittextclass"//启用更多的A标签的CSS类名
				//"fullfn": function () { alert("more") },
				//"lessfn": function () { alert("less") }
			}
		});



	//损失描述
	//     var limittext = $("#ChangeEDamageDes").
	//         limittext({
	//"limit": 10, "fill": "......",
	//             "morefn": {
	//"status": true, "moretext": "更多",
	//                 "lesstext": "隐藏",
	//                 cssclass: "limittextclass"//启用更多的A标签的CSS类名
	//                 //"fullfn": function () {alert("more")},
	//                 //"lessfn": function () {alert("less")}
	//             }
	//         });

	//     //设置
	//     var limittext = $("#ChangeEEarthDes").
	//         limittext({
	//"limit": 10, "fill": "......",
	//             "morefn": {
	//"status": true, "moretext": "更多",
	//                 "lesstext": "隐藏",
	//                 cssclass: "limittextclass"//启用更多的A标签的CSS类名
	//                 //"fullfn": function () {alert("more")},
	//                 //"lessfn": function () {alert("less")}
	//             }
	//         })



})
