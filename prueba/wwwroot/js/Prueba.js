$(window).on("load",
    function () {
        $(".E-loading").fadeOut("slow");


    });


var loadingTemplate =`
<div class="d-flex justify-content-center">
<div class="spinner-grow text-success" role="status">
</div>
</div>
`


$.ajaxSetup({
   
    statusCode: {
        401: function () {
           
            Swal.fire(
                'Sesión expiro',
                'Su sesión a expirado',
                'info'
            ).then(function (result) {
                var url = serverUrl+"/Usuario/Account/Login";
                window.location.href = url;
            });
        },
        403: function () {
            Swal.fire({
                title: 'No se pudo ejecutar esta acción',
                html: "<p>No tienes permisos suficientes para ejecutar esta acción</p>",
                icon: 'error'
            });
        },
        404: function (result) {
            Swal.fire({
               title: 'Ha ocurrido un error',
                html: result.responseText,
                icon: 'error'
            });
        },
        500: function (result) {
            Swal.fire({
                title: 'Ha ocurrido un error',
                html: result.responseText,
                icon: 'error'
            });
        },
    },
    cache: false
});


$(document).on("click",".remark",function () {
    $(this).remove();
});



