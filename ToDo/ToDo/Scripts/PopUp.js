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
            location.href = "/Zadania/Zadanie"
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
            location.href = "/Zadania/Zadanie"
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
    }
}

const PopUpWindow = $('#popup');

PopUpWindow.dialog({
    autoOpen: false,
});

const PopUp = {
    OpenDialog: function (window) {
        if (WindowConfig.hasOwnProperty(window)) {
            PopUpWindow.children().each(function () {
                $(this).hide();
                if ($(this).attr('id') == `${window}PopUp`)
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
        error.append(`<p>${msg}</p>`)
        error.dialog( WindowConfig['Error'])
        error.dialog('open');
    }
};