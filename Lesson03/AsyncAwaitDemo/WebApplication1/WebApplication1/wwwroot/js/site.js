// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(() => {
    const SERVER_URL = 'http://localhost:19328';

    $('.dropdown-item-lang').click(function() {
        console.log();
        $.post(
            [SERVER_URL, 'Home', 'ChangeLanguage'].join('/'),
            { lang: $(this).data('lang') }
        ).done(() => {
            location.reload();
        }).fail(() => {
            // console.log('fail');
        });
    });
});