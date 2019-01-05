﻿const editable = {
    selected: '',
    Select: function (selectedName) {
        const editButton = $('#EditButton');
        if (this.selected == selectedName) {
            this.selected = '';
            editButton.removeClass('icon')
            editButton.addClass('disabled');
        }
        else {
            if (this.selected != '')
                $(`#${this.selected}`).prop('checked', false);
            this.selected = selectedName;
            editButton.removeClass('disabled');
            editButton.addClass('icon');
        }
    },
    IsDisableEdit: function () {
        return this.selected == '';
    }
}

function Select(select) {
    editable.Select(select.id);
}

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
            height: Utilities.ParseToRem(40),
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

    $(`#modal`).dialog( windowConfig[windowName] );

    if (windowName == 'Edit' && editable.IsDisableEdit())
        return;

    $(`#modal`).dialog('open');


    $(`#modal .ui-dialog-titlebar`).css(`display`, `none`);
}

function ChangeElementsPerSite(location) {    
    AjaxPost(location, $(`#viewPerSite`).val());
};

function ChangeActualSite(location) {
    AjaxPost(location, $(`#actualSite`).val());
};
function PreviousSite(location) {
    const position = parseInt($(`#actualSite`).val()) - 1;
    AjaxPost(location, position);
};
function NextSite(location) {
    const position = parseInt($(`#actualSite`).val()) + 1;
    AjaxPost(location, position);
};

function PreviousInList(location, id) {
    AjaxPost(location, id);
}
function NextInList(location, id) {
    AjaxPost(location, id);
}

function Delete(location, id) {
    AjaxPost(location, id);
}

function AjaxPost(location, data) {
    console.log(data);
    $.ajax({
        type: 'POST',
        url: location,
        data: { element: data },
        dataType: "json"
    }).done(function (response) {
        if (response == "OK")
            window.location.reload(true);
    }).fail(function (response) {
        console.log('błąd...');
        console.log(response.responseText);
    });
    return false;
}


function SelectOneSiteOrAll() {
    $('#export-site').html(
        $(`#OneSite`).prop('checked')
            ? "Aktualna<br>strona"
            : "Wszystkie strony"
        );    
}

