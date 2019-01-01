const defaultPosition = {
    my: 'center',
    at: 'center',
    of: window,
    collision: 'fit'
};


function OpenModalWindow(windowName, id) {
    const windowConfig = {
        'Add': {
            classes: {
                "ui-dialog": "ui-corner-all custom-red"
            },
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            height: 300,
            height: 100,
            draggable: true,
            position: defaultPosition
        },
        'Edit': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition
        },
        'Description': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition
        },
        'List': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition
        },
        'Tile': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition
        },
        'Export': {
            autoOpen: false,
            classes: {
                "ui-dialog": "ui-corner-all custom-red",
                "ui-dialog-titlebar": "none"
            },
            hide: 'puff',
            show: 'slide',
            minWidth: Utilities.ParseToRem(7.5),
            width: Utilities.ParseToRem(7.5),
            height: Utilities.ParseToRem(25.5),
            draggable: false,
            position: {
                of: $('#export_btn'),
                my: 'center bottom',
                at: 'center top',
                collision: 'flip'
            }
        }
    }

    $(`#modal`).children().hide();
    $(`#modal`).children(`div.${windowName}-modal`).show();

    console.log($(`#modal .ui-dialog-titlebar`));
    $(`#modal`).dialog( windowConfig[windowName] );

    $(`#modal`).dialog('open');


    $(`#modal .ui-dialog-titlebar`).css(`display`, `none`);
}

$('#addOpenModal').click(() => {
    console.log('aaaa');
});

console.log('test end');