
const editable = {
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
        console.log(this.selected);
    },
    GetID: function () {
        return editable.selected.replace('selected_', '');
    },
    IsDisableEdit: function () {
        return this.selected == '';
    }
}
 
if (sessionStorage.getItem('editable') !== null) {
    editable.selected = sessionStorage.getItem('editable');
    $(`#${editable.selected}`).prop('checked', true);
    console.log($(`#${editable.selected}`));
}

console.log(editable);

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
            width: 'auto',
            height: 'auto',
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
            width: 'auto',
            height: 'auto',
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
            width: 'auto',
            height: 'auto',
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

    if (windowName == 'Edit') {
        if (editable.IsDisableEdit()) {
            return;
        }

        if (sessionStorage.getItem('openWindow') !== null)
            sessionStorage.removeItem('openWindow');
        else 
            AjaxPost('ToDo/SetEdit', editable.GetID());
    }

    if (windowName == 'Description') {
        if (sessionStorage.getItem('openWindow') !== null)
            sessionStorage.removeItem('openWindow');
        else
            AjaxPost('ToDo/Description', id);
    }

    $(`#modal`).dialog('open');


    $(`#modal .ui-dialog-titlebar`).css(`display`, `none`);
}
$(document).ready(() => {
    if (sessionStorage.getItem('openWindow') !== null) {
        OpenModalWindow(sessionStorage.getItem('openWindow'));
    }
});


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


function Validate(method) {
    const form = $(`#${method}Form`); 
    const actionVal = $(`#Action${method}`).val();
    const topicVal = $(`#Topic${method}`).val();
    const startVal = $(`#Start${method}`).val();
    const endVal = $(`#End${method}`).val();
    const statusVal = $(`#ActualStatus${method}`).val();
    const priorityVal = $(`#ActualPriority${method}`).val();
    const progressVal = $(`#Progress${method}`).val();
    const descriptionVal = $(`#Description${method}`).val();

    const actionError = $(`#ErrorAction${method}`);
    const topicError = $(`#ErrorTopic${method}`);
    const startError = $(`#ErrorStart${method}`);
    const endError = $(`#ErrorEnd${method}`);
    const statusError = $(`#ErrorStatus${method}`);
    const priorityError = $(`#ErrorPriority${method}`);
    const progressError = $(`#ErrorProgress${method}`);
    const descriptionError = $(`#ErrorDescription${method}`);

    const nullError = 'To pole nie może być puste.';
    const oorError = 'To pole przekracza dozwoloną wartość (max 255 znaków).';
    const nullText = '';

    let isCorrect = true;
    let isNull = false;
    let outOfRange = false;



    if ((isNull = !actionVal) || (outOfRange = actionVal.length > 255)) {
        isCorrect = false;
        if (isNull)
            actionError.html(nullError);
        if (outOfRange)
            actionError.html(oorError);
    } else
        actionError.html(nullText);

    if ( (isNull = !topicVal) || (outOfRange = topicVal.length > 255) ) {
        isCorrect = false;
        if (isNull)
            topicError.html(nullError);
        if (outOfRange)
            topicError.html(oorError);
    } else
        topicError.html(nullText);
  

    if ( isNull = !statusVal ) {
        isCorrect = false;
        if (isNull)
            statusError.html(nullError);
    } else
        statusError.html(nullText);



    if (isNull = !startVal) {
        isCorrect = false;
        if (isNull)
            startError.html(nullError);
    } else
        startError.html(nullText);

    if (isNull = !endVal) {
        isCorrect = false;
        if (isNull)
            endError.html(nullError);
    } else
        endError.html(nullText);

    if (isNull = !priorityVal) {
        isCorrect = false;
        if (isNull)
            priorityError.html(nullError);
    } else
        priorityError.html(nullText);
    

    if (isNull = !statusVal) {
        isCorrect = false;
        if (isNull)
            statusError.html(nullError);
    } else
        statusError.html(nullText);

    if (isNull = !progressVal) {
        isCorrect = false;
        if (isNull)
            progressError.html(nullError);
    } else
        progressError.html(nullText);


    if (outOfRange = descriptionVal.length > 255) {
        isCorrect = false;
        if (outOfRange) 
            descriptionError.html(oorError);
    } else
        descriptionError.html(nullText);

    if (isCorrect) {
        console.log('test1');
        console.log(isCorrect);
        form.submit();
    }
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
        
        if (response == "OK open Edit") {
            sessionStorage.setItem("openWindow", "Edit");
            sessionStorage.setItem("editable", editable.selected);

            window.location.reload(true);
        }

        if (response == "OK open Description") {
            sessionStorage.setItem("openWindow", "Description");
            sessionStorage.setItem("editable", editable.selected);

            window.location.reload(true);
        }
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

