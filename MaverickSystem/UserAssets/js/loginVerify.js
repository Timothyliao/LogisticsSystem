function onChange(contxt) {
	var id = "#" + contxt.id;
	var tip = "#tip_" + contxt.id;
	if ($(id).val() == "") {
		if (!$(tip).hasClass("tips_c")) {
			$(tip).addClass("tips_c");
		}
	} else
		$(tip).removeClass("tips_c");
};

function Import(contxt) {
	var id = "#" + contxt.id;
	var tip = "#tip_" + contxt.id;
	if ($(id).val() != "" && $(tip).hasClass("tips_c")) {
		$(tip).removeClass("tips_c");
	}
};

function Delete(contxt) {
	var id = "#" + contxt.id;
	var tip = "#tip_" + contxt.id;
	if ($(id).val() == "" && !$(tip).hasClass("tips_c")) {
		$(tip).addClass("tips_c");
	}
};

function VerifyEmail(contxt) {
	var id = contxt.id;
	var tip = "#tip_" + contxt.id;
	var email = $("#" + id).val();
	var pattern = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
	if (!pattern.test(email)) {
		if (!$(tip).hasClass("tips_c")) {
			$(tip).addClass("tips_c");
		}
	} else
		$(tip).removeClass("tips_c");
}

function PwdCompare(subject, comparing) {
	var compare = "#" + comparing.id;
	var subj = "#" + subject.id;
	var tip = "#tip_" + subject.id;
	if ($(compare).val() != $(subj).val()) {
		if (!$(tip).hasClass("tips_c")) {
			$(tip).addClass("tips_c");
		}
	} else
		$(tip).removeClass("tips_c");
}
