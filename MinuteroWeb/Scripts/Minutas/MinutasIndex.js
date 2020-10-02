MinutasIndex = (function () {
    'use strict';

    // Private variables
    var pageModel = {
        getMinutaURL: "",
        getUsuariosCopadosURL: "",
        getUsuariosEnFaltaURL: "",
        sendMinutaURL: "",
        sentFantasmaURL: "",
        marcarSendLaterURL: "",
        marcarStudyDayURL: "",
        marcarSickURL: "",
        marcarLicenseURL: "",
        marcarHolidaysURL: ""
    };

    // Private methods

    function onRefreshClicked(){
        location.reload(true);
    }

    function getPartialViewEstado() {
        return $('#partialEstado');
    }

    function getPartialViewGeneracionMinuta() {
        return $('#partialGeneracionMinuta');
    }

    function getUsuariosCopadosGrid() {
        return $('#grillaUsuariosCopados');
    }

    function getGrillaUsuariosEnFalta() {
        return $('#grillaUsuariosEnFalta');
    }

    function getBotonEstado() {
        return $('#btnEstado');
    }

    function getBotonGeneracionMinuta() {
        return $('#btnGeneracionMinuta');
    }
    
    function getBotonEnviarMinuta() {
        return $('#btnEnviarMinuta');
    }

    function getBotonEnviarFantasma() {
        return $('#fantasmaCentrado');
    }
    
    function getContenedorMinuta() {
        return $('#divContenidoMinutaConFormato');
    }

    function getDivMinuta() {
        return document.getElementById('divContenidoMinutaConFormato');
    }

    function configureBotonEstado() {
        getBotonEstado().on('click', botonEstadoClicked);
    }

    function configureBotonGeneracionMinuta() {
        getBotonGeneracionMinuta().on('click', botonGeneracionMinutaClicked);
    }
    
    function configureBotonEnviarMinuta() {
        getBotonEnviarMinuta().on('click', botonEnviarMinutaClicked);
    }

    function configureBotonEnviarFantasma() {
        getBotonEnviarFantasma().on('click', botonEnviarFanastmaClicked);
    }

    function botonGeneracionMinutaClicked(e) {
        if (getGrillaUsuariosEnFalta().data("kendoGrid").dataSource.view().length > 0) {
            alert("No se puede generar la minuta hasta que todos los usuarios tengan cargada la suya. Utilice los botones en la grilla de Usuarios Faltantes para cargar las minutas o persigalos con un fantasma.")
            return;
        }

        getBotonGeneracionMinuta().addClass('active');
        getBotonEstado().removeClass('active');

        GenerarVistaPreviaMinuta();

        getPartialViewGeneracionMinuta().show();
        getPartialViewEstado().hide();
    }

    function botonEstadoClicked(e) {
        getBotonGeneracionMinuta().removeClass('active');
        getBotonEstado().addClass('active');
        getPartialViewGeneracionMinuta().hide();
        getPartialViewEstado().show();
    }

    function botonEnviarFanastmaClicked(e) {

        if (!pageModel.esEncargado) {
            alert('¡No eres el encargado de tocar esos botones! ¡Vete a jugar a otro lado!');
            return;
        }

        $.ajax({
            url: pageModel.sentFantasmaURL,
            type: 'POST',
            cache: false,
            success: function (minuta) {
                alert('Nuestro fantasma ya fue a visitar a tu amigo');
            },
            error: function () {
                alert('Ocurrió un error al tratar de enviar el recordatorio de envio de minuta');
            }
        });
    }    

    function botonEnviarMinutaClicked(e) {
        if (confirm('Por favor, revise la vista previa de la minuta y si está seguro confirme su envío.')) {
            $.ajax({
                url: pageModel.sendMinutaURL,
                type: 'POST',
                cache: false,
                success: function () {
                    alert('La minuta fue enviada exitosamente');
                },
                error: function () {
                    alert('Ocurrió un error al tratar de enviar la minuta');
                }
            });
        }      
    }

    function GenerarVistaPreviaMinuta() {
        //clear content
        getContenedorMinuta().html("");

        $.ajax({
            url: pageModel.getMinutaURL,
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: {},
            success: function (minuta) {
                getContenedorMinuta().html(minuta);
                getBotonEnviarMinuta().removeAttr('disabled');
            },
            error: function () {
                alert('Ocurrió un error al tratar de armar la minuta');
            }
        });
    }

    function configureUsuariosCopadosGrid() {
        getUsuariosCopadosGrid().kendoGrid({
            dataSource: {
                transport: {
                    read: pageModel.getUsuariosCopadosURL,
                    dataType: "json"
                }
            },
            schema: {
                model: {
                    fields: {
                        nombreAMostrar: { type: "string" },
                        codigo: { type: "string" },
                        mail: { type: "string" }
                    }
                }
            },
            scrollable: false,
            columns: [{
                field: "nombreAMostrar",
                title: "Description",
                headerAttributes: {
                    style: "display: none "
                }
            }
            ]
        });
    }

    function configureUsuariosEnFaltaGrid() {
        getGrillaUsuariosEnFalta().kendoGrid({
            dataSource: {
                transport: {
                    read: pageModel.getUsuariosEnFaltaURL,
                    dataType: "json"
                }
            },
            schema: {
                model: {
                    fields: {
                        nombreAMostrar: { type: "string" },
                        codigo: { type: "string" },
                        mail: { type: "string" }
                    }
                }
            },
            scrollable: false,
            columns: [{
                field: "nombreAMostrar",
                title: "Description",
                width: "220px",
                headerAttributes: {
                    style: "display: none "
                }
            },
            {
                title: "Mas Tarde",
                template: "<a href=\"\\#\" onclick=\"MinutasIndex.botonMarcarSendLaterClicked('#= codigo #')\" title='Will send it later'> <i class='fa fa-clock-o fa-2x'> </i> </a> ",
                headerAttributes: {
                    style: "display: none"
                }
            },
            {
                title: "Dia de Estudio",
                template: "<a href=\"\\#\" onclick=\"MinutasIndex.botonMarcarStudyDayClicked('#= codigo #')\" title='Study Day'> <i class='fa fa-pencil fa-2x'> </i> </a> ",
                headerAttributes: {
                    style: "display: none"
                }
            },
            {
                title: "Licencia",
                template: "<a href=\"\\#\" onclick=\"MinutasIndex.botonMarcarLicenseClicked('#= codigo #')\" title='On License'> <i class='fa fa-wheelchair fa-2x'> </i> </a> ",
                headerAttributes: {
                    style: "display: none"
                }
            },
            {
                title: "Enfermo",
                template: "<a href=\"\\#\" onclick=\"MinutasIndex.botonMarcarSickClicked('#= codigo #')\" title='Sick'> <i class='fa fa-medkit fa-2x'> </i> </a> ",
                headerAttributes: {
                    style: "display: none"
                }
            },
            {
                title: "Vacaciones",
                template: "<a href=\"\\#\" onclick=\"MinutasIndex.botonMarcarHolidaysClicked('#= codigo #')\" title='On Holidays'> <i class='fa fa-sun-o fa-2x'> </i> </a> ",
                headerAttributes: {
                    style: "display: none"
                }
            }
            ]
        });
    }

    function botonMarcarSendLaterClicked(codigoUsuario) {
        postAjaxConActualizacionGrillaUsuarios(pageModel.marcarSendLaterURL, codigoUsuario, 'Ocurrió un error al tratar de grabar la marca Enviara su minuta luego en el usuario elegido');
    }

    function botonMarcarStudyDayClicked(codigoUsuario) {
        postAjaxConActualizacionGrillaUsuarios(pageModel.marcarStudyDayURL, codigoUsuario, 'Ocurrió un error al tratar de grabar la marca Día de estudio en el usuario elegido');
    }

    function botonMarcarLicenseClicked(codigoUsuario) {
        postAjaxConActualizacionGrillaUsuarios(pageModel.marcarLicenseURL, codigoUsuario, 'Ocurrió un error al tratar de grabar la marca Se encuentra de licencia en el usuario elegido');
    }

    function botonMarcarSickClicked(codigoUsuario) {
        postAjaxConActualizacionGrillaUsuarios(pageModel.marcarSickURL, codigoUsuario, 'Ocurrió un error al tratar de grabar la marca Se encuentra enfermo en el usuario elegido');
    }

    function botonMarcarHolidaysClicked(codigoUsuario) {
        postAjaxConActualizacionGrillaUsuarios(pageModel.marcarHolidaysURL, codigoUsuario, 'Ocurrió un error al tratar de grabar la marca Se encuentra de vacaciones en el usuario elegido');
    }

    function postAjaxConActualizacionGrillaUsuarios(url, codigoUsuario, mensajeError) {
        if (!pageModel.esEncargado) {
            alert('¡No eres el encargado de tocar esos botones! ¡Vete a jugar a otro lado!');
            return;
        }

        url = url.replace("codigoUsuarioValue", codigoUsuario);

        $.ajax({
            url: url,
            type: 'POST',
            cache: false,
            success: function (minuta) {
                actualizarGrillasUsuarios();
            },
            error: function () {
                alert(mensaje);
            }
        });
    }

    function actualizarGrillasUsuarios() {
        getGrillaUsuariosEnFalta().data('kendoGrid').dataSource.read();
        getGrillaUsuariosEnFalta().data('kendoGrid').refresh();

        getUsuariosCopadosGrid().data('kendoGrid').dataSource.read();
        getUsuariosCopadosGrid().data('kendoGrid').refresh();
    }

    function configureControls() {
        configureBotonEstado();
        configureBotonGeneracionMinuta();
        configureBotonEnviarMinuta();
        configureUsuariosCopadosGrid();
        configureUsuariosEnFaltaGrid();
        configureBotonEnviarFantasma();
    }

    // Public methods
    return {
        botonMarcarSendLaterClicked: botonMarcarSendLaterClicked,
        botonMarcarStudyDayClicked: botonMarcarStudyDayClicked,
        botonMarcarSickClicked: botonMarcarSickClicked,
        botonMarcarHolidaysClicked: botonMarcarHolidaysClicked,
        botonMarcarLicenseClicked: botonMarcarLicenseClicked,

        initialize: function (getMinutaURL, getUsuariosCopadosURL, getUsuariosEnFaltaURL, sendMinutaURL, sentFantasmaURL,
                              marcarSendLaterURL, marcarStudyDayURL, marcarSickURL, marcarLicenseURL, marcarHolidaysURL, esEncargado) {
            pageModel.getMinutaURL = getMinutaURL;
            pageModel.getUsuariosCopadosURL = getUsuariosCopadosURL;
            pageModel.getUsuariosEnFaltaURL = getUsuariosEnFaltaURL;
            pageModel.sendMinutaURL = sendMinutaURL;
            pageModel.sentFantasmaURL = sentFantasmaURL;
            pageModel.marcarSendLaterURL = marcarSendLaterURL;
            pageModel.marcarStudyDayURL = marcarStudyDayURL;
            pageModel.marcarSickURL = marcarSickURL;
            pageModel.marcarLicenseURL = marcarLicenseURL;
            pageModel.marcarHolidaysURL = marcarHolidaysURL;
            pageModel.esEncargado = esEncargado == 'True';

            configureControls();
        }
    };    
})();