var PageSize = 15
var pagerNum = 5
var pageCount = 0

function InitData(_pageCount) {
    pageCount = _pageCount
    InitTable()
    InitPager(0)
}

function InitTable() {
    $("#datatable-buttons .datarow").each(function (i) {
        if (i >= PageSize) {
            $(this).hide()
        } else
            $(this).show()
    })
}

function InitPager(pageIndex) {
    var str = ''
    str += '<li class="paginate_button page-item previous disabled" id="datatable-buttons_previous">' +
        '<a href=""><i class="mdi mdi-chevron-left"></i></a></li>'
    if (pageCount <= pagerNum) {
        for (var i = 1; i <= pageCount; i++) {
            if (i - 1 == pageIndex) {
                str += '<li class="paginate_button page-item active"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
            else {
                str += '<li class="paginate_button page-item"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
        }
    }
    else if ((pageIndex + parseInt(pagerNum / 2) + 1) >= pageCount) {
        for (var i = pageCount - (pagerNum - 1); i <= pageCount; i++) {
            if (i - 1 == pageIndex) {
                str += '<li class="paginate_button page-item active"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
            else {
                str += '<li class="paginate_button page-item"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
        }
    }
    else if (pageIndex < parseInt(pagerNum / 2) + 1) {
        for (var i = 1; i <= pagerNum; i++) {
            if (i - 1 == pageIndex) {
                str += '<li class="paginate_button page-item active"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
            else {
                str += '<li class="paginate_button page-item"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
        }
    }
    else {
        for (var i = pageIndex - parseInt(pagerNum / 2) + 1, end = pageIndex + parseInt(pagerNum / 2) + 1; i <= end; i++) {
            if (i - 1 == pageIndex) {
                str += '<li class="paginate_button page-item active"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
            else {
                str += '<li class="paginate_button page-item"><a href="javascript:ChangePage(' + i + ');" class="page-link">' + i + '</a></li>'
            }
        }
    }
    str += '<li class="paginate_button page-item next disabled" id="datatable-buttons_next">' +
        '<a href=""><i class="mdi mdi-chevron-right"></i></a></li>'

    $('#pager').html(str)
}

function ChangePage(index) {
    $("#datatable-buttons .datarow").each(function (i) {
        if (i >= PageSize * (index - 1) && i < PageSize * index)
            $(this).show()
        else
            $(this).hide()
    })
    InitPager(index - 1)
}

$('#showRow').change(function () {
    let rowNum = Number($(this).val())
    pageCount = Math.ceil(PageSize * pageCount / rowNum)
    PageSize = rowNum
    InitTable()
    InitPager(0)
})

$('#osearch').bind('input propertychange', function () {
    let searchText = $(this).val()
    let matchNum = 0
    $('#datatable-buttons .datarow').each(function (i) {
        let isMatch = false
        $(this).children().each(function () {
            if ($(this).text().search(searchText) != -1) {
                isMatch = true
                matchNum++
                return false
            }
        })
        if (isMatch)
            $(this).show()
        else
            $(this).hide()
    })
    pageCount = Math.ceil(matchNum / PageSize)
    InitPager(0)
    if (searchText == '' || searchText == null)
        InitTable()
})
