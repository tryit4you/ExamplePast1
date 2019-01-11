$(function () {
    register();
    getAllData();
});
function register() {

    $('.showImport').off('click').on('click', function (e) {
        e.preventDefault();
        var tableName = $(this).data('id');
        var dataName = $('#databaseName').val();
        $('#tableName').val(tableName);
        $('#dataName').val(dataName);
        $('.import-area').removeClass('hidden').fadeIn(2000);
    });
    $('.filter').off('click').on('click', function (e) {
        var tableName = $(this).data('id');
        var dataName = $('#databaseName').val();
        window.open('/table/filter?data=' + dataName + "&table="+tableName,'_self');
    });
    $('.importData').off('click').on('click', function (e) {
        e.preventDefault();
        var fileUrl = $('#fileImport').val();
        if (fileUrl === '' | fileUrl === undefined) {
            M.toast({ html: 'Vui lòng chọn file dữ liệu', classes: 'rounded  orange  lighten-1' });
        } else {
            
            importData(fileUrl);
        }
    });

    $('.btn-delete').off('click').on('click', function (e) {
        e.preventDefault();
        var tableName = $(this).data('id');

        deleteTable(tableName);

    });

    $('#searchRecord').off('click').on('click', function (res) {
        var lat = $('#lat').val();
        var lng = $('#lng').val();
        var bk = $('#bk').val();
        if (lat === '' || lng === '' || bk === '') {
            M.toast({ html: 'Vui lòng nhập đầy đủ giả trị!', classes: 'rounded  orange  lighten-1' });
        } else {
            searchRecord(lat, lng, bk);
        }
    });
}
function searchRecord(lat, lng, bk) {
    $.ajax({
        url: '/table/SearchRecord',
        data: {
            lat: lat,
            lng: lng,
            bk: bk
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                M.toast({ html: 'Thêm dữ liệu thành công!', classes: 'rounded  blue  lighten-1' });
            }
        }
    });
}
function importData(fileUrl) {
    var tableName = $('#tableName').val();
    var dataName = $('#dataName').val();
    $.ajax({
        url: '/table/importData',
        data: {
            tableName: tableName,
            dataName:dataName,
            fileUrl: fileUrl
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            $('#fileImport').val('');
            if (res.status === false) {
                M.toast({ html: res.message, classes: 'rounded  red  lighten-1' });
            } else {
                M.toast({ html: "Thời gian truy vấn " + res.timequery + " (ms)", classes: 'rounded  blue  lighten-1' });
                M.toast({ html: res.rowquery, classes: 'rounded  blue  lighten-1' });
                $('.import-area').addClass('hidden').fadeOut(2000);
            }
        }
    });
}
function deleteTable(tableName) {
    var databaseName = $('#databaseName').val();
    $.ajax({
        url: '/table/deleteTable',
        data: {
            tableName: tableName,
            dataName: databaseName
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                M.toast({ html: 'Xóa thành công!', classes: 'rounded  blue  lighten-1' });
                getAllData();
            } else {
                M.toast({ html: 'Xóa không thành công!', classes: 'rounded  red  lighten-1' });
            }
        }
    });
}

function getAllData() {
    var dataName = $('#databaseName').val();
    $.ajax({
        url: '/table/ViewTable',
        data: {
            dataName: dataName
        },
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            var data = response.data;
            var html = '';

            var template = $('#html-template').html();
            $.each(data, function (i, item) {
                html += Mustache.render(template, {
                    DatabaseName: item.DatabaseName,
                    TableName: item.TableName
                });
            });
            $('#dataRender').html(html);
            register();
        }
    });
}