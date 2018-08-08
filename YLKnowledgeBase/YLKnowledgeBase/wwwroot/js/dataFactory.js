class ServiceAbstract {
    constructor() {
        this.array;
        this.getData = function (url, success, data) {
            $.ajax({
                url: url,
                type: 'GET',
                data: date,
                success: success
            });
        };
        this.postData = function (url, success, data) {
            $.ajax({
                url: url,
                type: 'POST',
                data: date,
                success: success
            });
        };
    }

    static toArray(data) {
        this.array = data.map(x => x);
    }

    static printArray() {
        console.log(this.array);
    }
}

class CategoryService extends ServiceAbstract {
    constructor() {
        super();
        this.getData = function () {
            $.ajax({
                url: "api/Categories",
                data: {},
                success: function (response) {
                    CategoryService.toArray(response);
                    CategoryService.createButtons('categories');
                    CategoryService.printArray();
                }
            });
        };
    }

    static createButtons(elementId) {
        this.array.forEach(function (item) {

            if (item.notes !== null) {
                var notesArray = item.notes.map(x => x);
                notesArray.forEach(function (note) {
                    console.log(note);
                });
            }

            var element = document.getElementById(elementId);
            var div = document.createElement("div");
            div.setAttribute("class", "category");
            div.setAttribute("id", item.categoryId);
            //console.log(element);
            //element.innerHTML += '<div class="option-' + elementId + '"><button>' + item.name + '</button></div>';

            var btn = document.createElement("button");
            btn.setAttribute("class", "option-" + elementId);
            var t = document.createTextNode(item.name);
            btn.appendChild(t);
            btn.setAttribute("onclick", 'changeActivity("' + item.categoryId +'", "category-active")');
            //btn.addEventListener("click", ServiceAbstract.changeActivity(div));
            div.appendChild(btn);

            var btnp = document.createElement("button");
            btnp.setAttribute("class", "btn-plus");
            var tp = document.createTextNode("+");
            btnp.appendChild(tp);
            div.appendChild(btnp);

            var notes = document.createElement("div");
            notes.setAttribute("class", "notes");
            //notes.setAttribute("onclick", 'changeActivity("' + item.categoryId + '", "category-active")');
            div.appendChild(notes);

            element.appendChild(div);
        });
    }

}

class NoteService extends ServiceAbstract {
    constructor(divId) {
        super();
        this.divId = divId;
        this.getData = function () {
            $.ajax({
                url: "api/Notes/",
                data: {},
                success: function (response) {
                    //console.log(this);
                    NoteService.toArray(response);
                    NoteService.printArray();
                    NoteService.toCategories();
                }
            });
        };
    }

    static createButton(divId) {
        var element = document.getElementById(elementId);
        var divNotes = element.getElementsByClassName('notes');
    }

    static toCategories() {
        console.log();
    }
}

function changeActivity(divId, actClass) {
    console.log(divId);
    var div = document.getElementById(divId);
    if (div.classList.contains(actClass)) {
        div.classList.remove(actClass);
    }
    else {
        div.classList.add(actClass);
    }

    //const noteService = new NoteService(divId);
    //noteService.createButton(divId);
}

$(document).ready(function () {
    console.log("ready");
    const categoryService = new CategoryService();
    categoryService.getData();
    const noteService = new NoteService();
    noteService.getData();
});

function getCount(data) {
    const el = $('#counter');
    let name = 'object';
    if (data) {
        if (data > 1) {
            name = 'objects';
        }
        el.text(data + ' ' + name);
        console.log(data + ' ' + name);
    } else {
        el.html('No ' + name);
    }
    
}

function Category(id, name) {
    this.id = id;
    this.name = name;
    console.log(this.name);
}

/*
function getData(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            //$('#todos').empty();
            getCount(data.length);
            addToArray();
            //addToArray(data);

            $.each(data, function (key, item) {
                const checked = item.isComplete ? 'checked' : '';

                $('<tr><td><input disabled="true" type="checkbox" ' + checked + '></td>' +
                    '<td>' + item.name + '</td>' +
                    '<td><button onclick="editItem(' + item.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteItem(' + item.id + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#todos'));
            });

            $.each(data, function (key, item) {
            });
        }
    });
}
*/

function addItem() {
    const item = {
        'name': $('#add-name').val(),
        'isComplete': false
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#add-name').val('');
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(dataList, function (key, item) {
        if (item.id === id) {
            $('#edit-name').val(item.name);
            $('#edit-id').val(item.id);
            $('#edit-isComplete').val(item.isComplete);
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const item = {
        'name': $('#edit-name').val(),
        'isComplete': $('#edit-isComplete').is(':checked'),
        'id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}

//https://docs.microsoft.com/pl-pl/aspnet/core/tutorials/web-api-vsc?view=aspnetcore-2.0

/*
 * class ServiceAbstract {
    constructor(type) {
        this.type = type;
    }

    fetchData = function({
      url = "https://reqres.in/api/users",
      success = (data) => console.log(data),
      type = 'GET',
      data = {}
    }) {
      $.ajax({
        url,
        type,
        data,
        success
      });
    }
}

class CategoryService extends ServiceAbstract {
    constructor(type) {
      super(type);
    }
}

const service = new CategoryService('categories');
service.fetchData({});
*/