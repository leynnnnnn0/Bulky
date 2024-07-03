var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/Admin/Product/Getall',
        },
        "columns": [
            { data: 'title' },
            { data: 'author' },
            { data: 'description' },
            { data: 'category.categoryName' },
            {
                data: 'id',
                "render": function (data) {
                    return ` <div class="w-75 btn-group">
                            <a href="/Admin/Product/upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick=deleteRow("/Admin/Product/delete/${data}") class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i> Delete
                            </a>
                        </div>`
                }
            }
        ]
    });
}

const deleteRow = (url) => {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    Swal.fire({
                        
                        title: "Deleted!",
                        text: data.message,
                        icon: "success"
                    });
                }
            })
        }
    });
}

