// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $(document).on('click', '.page-link', function (e) {
        e.preventDefault();

        var url = $(this).attr('href');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $('#movie-list').html(data);
                history.pushState(null, '', url);
            }
        });
    });
});

//$(document).on('click', '.page-link', function (event) {
//    event.preventDefault();

//    const url = $(this).attr('href');
//    $('#movie-list').load(url + ' #movie-list');
//    $('#pager').load(url + ' #pager');
//});