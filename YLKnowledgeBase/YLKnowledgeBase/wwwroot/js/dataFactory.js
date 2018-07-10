class ServiceAbstract {
    constructor() {
        this.getData = function (url, type, success, data) {
            $.ajax({
                url: url,
                type: type,
                data: date,
                success: success
            });
        };
    }

}

createButton2 = function (data) {
    $.each(data, function (key, item) {
        $('<button>' + item.name + '</button>').appendTo($('#categories'));
    });
};

class CategoryService extends ServiceAbstract {
    constructor() {
        super();
        this.getData = function () {
            $.ajax({
                url: "api/Categories",
                type: 'GET',
                data: {},
                success: function (response) { CategoryService.createButton(response); }
                //success: function (response) { CategoryService.metoda(response); }
                //success: function (response) { createButton(response);}
            });
        };
    }

    static metoda(data) {
        console.log(data);
    }

    static createButton (data) {
        $.each(data, function (key, item) {
            $('<div class="option-categories"><button class="option-categories">' + item.name + '</button></div>').appendTo($('#categories'));
        });
    }

}



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

$(document).ready(function () {
    console.log("ready");
    const categoryService = new CategoryService();
    categoryService.getData();
});

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