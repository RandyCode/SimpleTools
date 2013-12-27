/// <reference path="jquery-1.8.0.min.js" />
//好處就是設置主題 只需要修改這裡的屬性
var theme={
    //tab property
    tabIndex: -1,
    tabInColor: "lightgreen",
    tabOutColor:"green"
};

$.fn.extend({
    tabClick: function () {
        $(this).find("div:first").show()
        var div = $(this).find("div");
        var li = $(this).find("li");
        $(this).find("li").each(function () {
            $(this).click(function () {
                li.css("background-color", theme.tabOutColor);
                $(this).css("background-color", theme.tabInColor);
                div.hide();
                var liIndex = $(this).index();
                theme.tabIndex = liIndex;
                $(div[liIndex]).show()
            })
        })
        return this;
    },
    tabHover: function () {
        $(this).find("li").each(function () {      
            $(this).hover(function () {
                if (theme.tabIndex == $(this).index()) return;
                $(this).css("background-color", theme.tabInColor);
            }, function () {
                if (theme.tabIndex == $(this).index()) return;
                $(this).css("background-color", theme.tabOutColor);
            })
        })
        return this;
    }
   
});

//(function aa() { alert("5")})()
