const WindowConfig = {
    'Add': {
        autoOpen: false,
        title: 'Dodaj nowe zadanie',
        modal: true,
        minWidth: '500px',
        maxWidth: '500px',
        width: '500px',
        hide: 'puff',
        show: 'puff',
        beforeClose: function (event, ui) {
        }
    },
    'Edit': {
        autoOpen: false,
        title: 'Edytuj zadanie',
        modal: true,
        minWidth: '500px',
        maxWidth: '500px',
        width: '500px',
        hide: 'puff',
        show: 'puff',
        close: function (event, ui) {
            location.href = "/Zadania/Zadanie";
        }
    },
    'Error': {
        autoOpen: true,
        title: 'Błąd',
        modal: true,
        hide: 'clip',
        show: 'shake',
        buttons: [
            {
                text: "OK",
                click: function () {
                    $(this).dialog('close');
                }
            }]
    },
    'Description': {
        autoOpen: true,
        title: 'Szczegóły zadania',
        modal: true,
        hide: 'puff',
        show: 'puff'
    },
    'Export': {
        autoOpen: false,
        classes: {
            "ui-dialog": "ui-corner-all custom-red",
            "ui-dialog-titlebar": "none"
        },
        hide: 'puff',
        show: 'slide',
        height: "auto",
        minWidth: 150,
        maxWidth: 150,
        width: 150,
        draggable: false,
        position: {
            of: $('#export_btn'),
            my: 'center bottom',
            at: 'center top',
            collision: 'flip'
        }
    }
};

const PopUpWindow = $('#popup');

PopUpWindow.dialog({
    autoOpen: false
});

const PopUp = {
    OpenDialog: function (window) {
        if (WindowConfig.hasOwnProperty(window)) {
            PopUpWindow.children().each(function () {
                $(this).hide();
                if ($(this).attr('id') === `${window}PopUp`)
                    $(this).show();
            });

            PopUpWindow.dialog( WindowConfig[window] );
            PopUpWindow.dialog('open');
        }
        else
            console.log(`Nie ma okna: ${window}`);
    },
    OpenErrorDialog: function (msg) {
        const error = $('#Error');

        error.empty();
        error.append(`<p>${msg}</p>`);
        error.dialog(WindowConfig['Error']);
        error.dialog('open');
    }
};