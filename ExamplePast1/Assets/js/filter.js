$(function () {
    register();
    getAllData();
});
function register() {
    $('#searchRecord').off('click').on('click', function (res) {
        var lat = $('#lat').val();
        var lng = $('#lng').val();
        var bk = $('#bk').val();
        if (lat === '' || lng === '' || bk === '') {
            M.toast({ html: 'Vui lòng nhập đầy đủ giả trị!', classes: 'rounded  orange  lighten-1' });
        } else {
            getAllData(lat, lng, bk);
        }
    }); $('#searchRecordCircle').off('click').on('click', function (res) {
        var lat = $('#latCircle').val();
        var lng = $('#lngCircle').val();
        var bk = $('#bkCircle').val();
        if (lat === '' || lng === '' || bk === '') {
            M.toast({ html: 'Vui lòng nhập đầy đủ giả trị!', classes: 'rounded  orange  lighten-1' });
        } else {
            getAllDataCircle(lat, lng, bk);
        }
    });
}

function getAllData(lat,lng,bk) {
    var dataName = $('#dataName').val();
    var tableName = $('#tableName').val();
    $.ajax({
        url: '/table/SearchRecord',
        data: {
            dataName: dataName,
            tableName: tableName,
            lat: lat,
            lng: lng,
            bk: bk
        },
        type: 'POST',
        dataType: 'json',
        success: function (response) {

            var data = response.data;
            var html = '';
            if (data.length === 0 || data.length === undefined) {
                console.log('co 0 row');
            } else {
                M.toast({ html: 'Tìm thấy ' + data.length +' dòng', classes: 'rounded  orange  lighten-1' });

                var template = $('#html-template').html();
                $.each(data, function (i, item) {
                    html += Mustache.render(template, {
                        TenCayXanh: item.TenCayXanh ,
                        Lat: item.Lattitude ,
                        Lng: item.Longtitude 
                    });
                });

                $('#dataRender').html(html);
                register();
            }
           
        }
    });
}
function getAllDataCircle(lat, lng, bk) {
    var dataName = $('#dataName').val();
    var tableName = $('#tableName').val();
    $.ajax({
        url: '/table/SearchRecordCircle',
        data: {
            dataName: dataName,
            tableName: tableName,
            lat: lat,
            lng: lng,
            bk: bk
        },
        type: 'POST',
        dataType: 'json',
        success: function (response) {

            var data = response.data;
            var html = '';
            if (data.length === 0 || data.length === undefined) {
                console.log('co 0 row');
            } else {
                M.toast({ html: 'Tìm thấy ' + data.length + ' dòng', classes: 'rounded  orange  lighten-1' });

                var template = $('#html-template').html();
                $.each(data, function (i, item) {
                    html += Mustache.render(template, {
                        TenCayXanh: item.TenCayXanh,
                        Lat: item.Lattitude,
                        Lng: item.Longtitude
                    });
                });

                $('#dataRender').html(html);
                register();
            }

        }
    });
}