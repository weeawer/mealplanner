const sql = require('mssql');
const ExcelJS = require('exceljs');

const config = {
    user: 'sa',
    password: 'YouSawSaw_88',
    server: '127.0.0.1', // Используйте 'localhost\\instance' для подключения к именованному экземпляру
    database: 'vtmealz',
    options: {
        encrypt: true,
        trustServerCertificate: true
    }
};

async function exportToExcel() {
    try {
        // Подключение к базе данных
        await sql.connect(config);

        // Проверка существования таблицы
        const tableCheck = await sql.query(`
            SELECT name
            FROM sys.tables
            WHERE name = 'ChoiseMeals';
        `);

        if (tableCheck.recordset.length === 0) {
            throw new Error("Table 'ChoiseMeals' does not exist in the database.");
        }

        // SQL-запрос для получения данных
        const result = await sql.query(`
            SELECT 
                cm.AppUserId AS AppUserId,
                c.Name AS CategoryName,
                m.Name AS MealName,
                m.Price AS MealPrice,
                cm.DayId AS DayId
            FROM 
                ChoiseMeals cm
            JOIN 
                Meals m ON cm.mealId = m.Id
            JOIN 
                Categories c ON m.categoryId = c.Id;
        `);

        // Создание нового Excel-файла
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet('Meals');

        // Добавление заголовков столбцов
        worksheet.columns = [
            { header: 'Имя пользователя', key: 'AppUserId' },
            { header: 'Категория', key: 'CategoryName' },
            { header: 'Блюдо', key: 'MealName' },
            { header: 'Цена', key: 'MealPrice' },
            { header: 'День недели', key: 'DayId' }
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
