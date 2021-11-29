function exportToExcel() {
    var printContent = document.getElementById('<%= pnlGridView.ClientID %>');
    var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');
    printWindow.document.write(printContent.innerHTML);
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
}