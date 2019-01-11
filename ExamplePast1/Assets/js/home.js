$(function () {
    register();
    getAllData();
});
function register() {
    $('.modal').modal();
    $('#createDatabase').off('click').on('click', function (e) {
        e.preventDefault();
        var dataName = $('#databaseName').val();
        if (dataName === '') {
            M.toast("Vui lòng nhập tên database");
        } else {

            createdDatabase(dataName);
        }
    });
    $('.createTable').off('click').on('click', function (e) {
        e.preventDefault();
        var dataName = $(this).data('id');
        $('#dataHide').val(dataName);
    });
    $('.viewTable').off('click').on('click', function (e) {
        e.preventDefault();
        var dataName = $(this).data('id');
        window.open('/table/index?dataName=' + dataName,'_self');
    });
    $('#btn-CreateTable').off('click').on('click', function (e) {
        e.preventDefault();
        var dataName = $("#dataHide").val();
        var tableName = $("#tableName").val();
        if (tableName === '') {
            M.toast({ html: 'Vui lòng nhập tên bảng', classes: 'rounded  orange  lighten-1' });
        } else {
            createTable(dataName, tableName);
        }
    });
    $('.importData').off('click').on('click', function () {
        var fileUrl = $('#fileImport').val();
        if (fileUrl === '' | fileUrl === undefined) {
            M.toast({ html: 'Vui lòng chọn file dữ liệu', classes: 'rounded  orange  lighten-1' });
        } else {
            importData(fileUrl);
        }
    });
    $('.btn-delete').off('click').on('click', function (e) {
        e.preventDefault();
        var dataName = $(this).data('id');
        deleteDataBase(dataName);
    });
   
}
function deleteDataBase(dataName) {
    $.ajax({
        url: '/home/deleteDatabase',
        data: { dataName: dataName },
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
function searchRecord(lat, lng, bk) {
    $.ajax({
        url: '/home/SearchRecord',
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
    $.ajax({
        url: '/home/importData',
        data: { fileUrl: fileUrl },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                M.toast({ html: 'Thêm dữ liệu thành công!', classes: 'rounded  blue  lighten-1' });
            }
        }
    });
}
function createdDatabase(dataName) {
    $.ajax({
        url: '/home/createdatabase',
        data: {
            dataName: dataName
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                $('#databaseName').val('');
                M.toast({ html: res.message, classes: 'rounded  blue  lighten-1' });
                getAllData();
            } else {
                $('#databaseName').val('');
                M.toast({ html: res.message, classes: 'rounded  orange  lighten-1' });


            }
        }
    });
}
function createTable(dataName, tableName) {
    $.ajax({
        url: '/table/createTable',
        data: {
            dataName: dataName,
            tableName: tableName
        },
        type: 'post',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                M.toast({ html: res.message, classes: 'rounded  blue  lighten-1' });
                $('.modal').modal().hide();
            } else {
                M.toast({ html: res.message, classes: 'rounded  orange  lighten-1' });
                $('.modal').modal().hide();
            }
        }
    });
}

function getAllData() {
    $.ajax({
        url: '/home/GetAllDatabase',
        type: 'POST',
        dataType: 'json',
        success: function (response) {
                var data = response.data;
            var html = '';
            
                var template = $('#html-template').html();
                $.each(data, function (i, item) {
                    html += Mustache.render(template, {
                        DatabaseId: item.DatabaseId,
                        DatabaseName: item.DatabaseName,
                        CreateDate: item.CreateDate
                    });
                });
            $('#dataRender').html(html);
            register();
        }
    });
}