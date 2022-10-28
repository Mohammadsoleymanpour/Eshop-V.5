
function orderChart(dataSales) { 
        const labels = [
            'شنبه',
            'یکشنبه',
            'دوشنبه',
            'سه شنبه',
            'چهارشنبه',
            'پنجشنبه',
            'جمعه'
        ];

        const chartdata = {
            labels: labels,
            datasets: [{
                label: 'My First dataset',
                backgroundColor: '',
                borderColor: 'rgb(255, 209, 36)',
                data: [0, 10, 5, 2, 20, 30, 45],
/*                data: [dataSales[0], dataSales[1], dataSales[2], dataSales[3], dataSales[4], dataSales[5], dataSales[6]]*/
            }]
        };

        const config = {
            type: 'line',
            data: chartdata,
            options: {}
        };
   
        const bar = new Chart(
            document.getElementById('bar'),
            config
        );


}

