$(document).ready(function () {
    loadTable();
});

function loadTable() {
    $('#myTable').DataTable({
        "ajax": { url: '/CompanyUsers/GetAll' },
        "columns": [
            { data: 'userid', "width": "15%" },
            { data: 'username', "width": "15%" }
        ]
    });
}