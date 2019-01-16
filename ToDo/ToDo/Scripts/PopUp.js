const WindowConfig = {
    'Add': {
        autoOpen: false,
        title: 'Dodaj nowe zadanie',
        modal: true,
        minWidth: '500px',
        maxWidth: '500px',
        width: '500px',
        hide: 'puff',
        show: 'puff'
    },
    'Edit': {
        autoOpen: false,
        title: 'Edit',
        modal: true,
        minWidth: '500px',
        maxWidth: '500px',
        width: '500px',
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
    }
};