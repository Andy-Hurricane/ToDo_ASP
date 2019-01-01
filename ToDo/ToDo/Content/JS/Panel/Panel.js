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
            position: defaultPosition,
            open: function (event, ui) {
                $('#modal').css('overflow', 'hidden');
            }
        },
        'Edit': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition,
            open: function (event, ui) {
                $('#modal').css('overflow', 'hidden');
            }
        },
        'Description': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition,
            open: function (event, ui) {
                $('#modal').css('overflow', 'hidden');
            }
        },
        'List': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition,
            open: function (event, ui) {
                $('#modal').css('overflow', 'hidden');
            }
        },
        'Tile': {
            autoOpen: false,
            hide: 'puff',
            show: 'slide',
            width: 300,
            height: 100,
            draggable: true,
            position: defaultPosition,
            open: function (event, ui) {
                $('#modal').css('overflow', 'hidden');
            }
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
            height: Utilities.ParseToRem(35.5),
            draggable: false,
            position: {
                of: $('#export_btn'),
                my: 'center bottom',
                at: 'center top',
                collision: 'flip'
            },
            open: function (event, ui) {
                $('#modal').css('overflow', 'hidden');
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

console.log($(`#viewPerSite`));

function ChangeElementsPerSite(test) {
    console.log('?!?!?!');
    console.log(test);
    const data = { perSite: $(`#viewPerSite`).val() };
    AjaxPost(test, data);
};

function ChangeActualSite() {
    console.log("zmieniam stronę.")
};

function AjaxPost(location, data) {
    console.log('Jestem?');
    $.ajax({
        url: location,
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (mydata) {
            console.log('Wysłało?');
            // history.pushState('', 'New URL: ' + href, href); // This Code lets you to change url howyouwant
        },
        error: function (test) {
            console.log(test.responseText);
        }
    });
    console.log('Yh...');
    return false;
}
