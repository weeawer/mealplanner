const sql = require('mssql');
const ExcelJS = require('exceljs');

const config = {
    user: 'sa',
    password: 'YouSawSaw_88',
    server: '127.0.0.1',
    database: 'vtmealzz',
    options: {
        encrypt: true,
        trustServerCertificate: true
    }
};

async function exportToExcel() {
    try {
        // Подключение к базе данных
        await sql.connect(config);

        // SQL-запрос для получения данных
        const result = await sql.query(`
            SELECT 
                c.Name AS CategoryName,
                m.Name AS MealName,
                m.Description AS MealDescription,
                m.Price AS MealPrice,
                SUM(CASE WHEN u.Shift = 1 THEN 1 ELSE 0 END) AS Shift1Count,
                SUM(CASE WHEN u.Shift = 2 THEN 1 ELSE 0 END) AS Shift2Count,
                SUM(CASE WHEN u.EmpType = 1 THEN 1 ELSE 0 END) AS ProductionCount,
                SUM(CASE WHEN u.EmpType = 2 THEN 1 ELSE 0 END) AS OfficeCount,
                COUNT(cm.MealId) AS TotalMeals,
                SUM(CASE WHEN u.EmpType = 1 THEN m.Price ELSE 0 END) AS ProductionSum,
                SUM(CASE WHEN u.EmpType = 2 THEN m.Price ELSE 0 END) AS OfficeSum,
                SUM(m.Price) AS TotalSum
            FROM 
                ChoiseMeals cm
            JOIN 
                Meals m ON cm.mealId = m.Id
            JOIN 
                Categories c ON m.categoryId = c.Id
            JOIN
                AspNetUsers u ON cm.AppUserId = u.Id
            GROUP BY 
                c.Name, m.Name, m.Description, m.Price;
        `);

        // Создание нового Excel-файла
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet('Meals');

        // Добавление заголовков столбцов
        worksheet.columns = [
            { header: 'Category Name', key: 'CategoryName' },
            { header: 'Meals Name', key: 'MealName' },
            { header: 'Meals Description', key: 'MealDescription' },
            { header: 'Meals Price', key: 'MealPrice' },
            { header: '1 смена', key: 'Shift1Count' },
            { header: '2 смена', key: 'Shift2Count' },
            { header: 'Производство', key: 'ProductionCount' },
            { header: 'Офис', key: 'OfficeCount' },
            { header: 'Всего блюд', key: 'TotalMeals' },
            { header: 'Сумма производство', key: 'ProductionSum' },
            { header: 'Сумма офис', key: 'OfficeSum' },
            { header: 'Сумма', key: 'TotalSum' }
        ];

        // Добавление строк данных
        result.recordset.forEach(record => {
            worksheet.addRow(record);
        });

        // Сохранение файла Excel
        await workbook.xlsx.writeFile('ChoiseMeals.xlsx');
        console.log('Data exported to ChoiseMeals.xlsx');
    } catch (err) {
        console.error('Error exporting data:', err);
    } finally {
        await sql.close();
    }
}

exportToExcel();