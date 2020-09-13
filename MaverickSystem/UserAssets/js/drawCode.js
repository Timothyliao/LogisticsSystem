function GetCode(authcode) {
	var canvas = document.querySelector("#" + authcode);
	var corlorArray = new Array("#ff793f", "#1abc9c", "#8e44ad", "#2980b9", "#d35400", "#c0392b"); //字体颜色
	var charArray = new Array('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p',  'r',
		's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
		'X', 'Y', 'Z',
		'0', '1', '2', '3', '4', '5', '6', '7', '8') //字符取值范围
	var roatArray = new Array(0, 2.5, 5, 7.5, 10, -1.5); //字体旋转角度
	var width = 200,
		height = 40;
	canvas.width = width;
	canvas.height = height;
	var codeL = 5; //字符数量
	var tempcode = '';
	var ctx = canvas.getContext("2d");
	var len = charArray.length;
	for (var i = 0; i < codeL; ++i) {
		var index = parseInt(Math.random() * len, 10); //得到随机字符
		var deg = (Math.random() * 45 * Math.PI) / 180; //得到随机弧度
		var ch = charArray[index];
		var x = 15 + i * 35 + Math.random() * 10;
		var y = 22 + Math.random() * 10;
		ctx.font = "bold 30px console";
		ctx.translate(x, y);
		ctx.rotate(deg);

		ctx.fillStyle = corlorArray[Math.floor(Math.random() * 6)];
		ctx.fillText(ch, 0, 0);
		ctx.rotate(-deg);
		ctx.translate(-x, -y);
		tempcode += ch.toLowerCase();
	}

	//显示噪线
	for (var i = 0; i < 7; ++i) {
		ctx.fillStyle = i % 2 == 0 ? "#34e7e4" :"#d2dae2";
		ctx.beginPath();
		ctx.moveTo(
			Math.random() * width,
			Math.random() * height
		);
		ctx.lineTo(
			Math.random() * width,
			Math.random() * height
		);
		ctx.stroke();
	}

	//画噪点

	for (var i = 0; i < 150; ++i) {
		ctx.fillStyle = corlorArray[Math.floor(Math.random() * 6)];
		ctx.beginPath();
		var x = Math.random() * width;
		var y = Math.random() * height;
		ctx.moveTo(x, y);
		ctx.lineTo(x + 1, y + 1);
		ctx.stroke();
	}
	return tempcode.toLowerCase();
};
