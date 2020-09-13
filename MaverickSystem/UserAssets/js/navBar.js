(function () {
    var d_menu = $(".dropdown-menu");
    var item = $(".s-n-s-l");
    var arrows = $(".nd-i");
    console.log(arrows);

    for (var i = 0, len = d_menu.length; i < len; i++) {
        d_menu[i].index = i;
        d_menu[i].flag = true;
        $(item[i]).hide();
        $(d_menu[i]).click(function () {
            let index = this.index;
            for (var j = 0, len = d_menu.length; j < len; j++) {
                if (j != index && !d_menu[j].flag) {
                    $(arrows[j]).css({ "transform": "rotate(0deg)" });
                    $(item[j]).slideUp();
                    d_menu[j].flag = !d_menu[j].flag;
                }
            }
            $(item[index]).slideToggle();
            if (d_menu[index].flag)
                $(arrows[index]).css({ "transform": "rotate(90deg)", "color": "#3498db" });
            else
                $(arrows[index]).css({ "transform": "rotate(0deg)", "color": "#6c757d" });
            d_menu[index].flag = !d_menu[index].flag;
        })
    }
})();   