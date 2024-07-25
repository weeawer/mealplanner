const xlsx = require('xlsx');
const sql = require('mssql');

// Конфигурация для подключения к MS SQL Server
const config = {
    user: 'sa',
    password: 'YouSawSaw_88',
    server: '127.0.0.1', // You can use 'localhost\\instance' to connect to named instance
    database: 'vtmeal',
    options: {
        encrypt: true, // For Azure SQL Database
        trustServerCertificate: true // Change to true for local dev / self-signed certs
    }
};

// Чтение Excel-файла
const workbook = xlsx.readFile('mealz.xlsx');

const sheetName = workbook.SheetNames[0];
const worksheet = workbook.Sheets[sheetName];

// Преобразование данных в JSON
const jsonData = xlsx.utils.sheet_to_json(worksheet);


// Вставка данных в таблицу
async function insertDataToDB() {
    try {
        // Подключение к базе данных
        await sql.connect(config);
        await new sql.Request().query('DELETE FROM dbo.Meals');

        // Перебор JSON-данных и вставка каждой записи
        for (const row of jsonData) {
            
            const request = new sql.Request();

            // Формирование запроса на вставку данных (на примере таблицы с двумя столбцами: Column1 и Column2)
            const query = `
                INSERT INTO dbo.Meals (Type, Name, Description, DayId)
                VALUES (@Type, @Name, @Description, @DayId)
            `;

            // Добавление параметров
            
            request.input('Type', sql.NVarChar, row.Type || 'default_type');
            request.input('Name', sql.NVarChar, row.Name || 'default_name');
            request.input('Description', sql.NVarChar, row.Description || 'default_description');
            request.input('DayId', sql.Int, row.DayId || 1);

            // Выполнение запроса
            await request.query(query);
        }

        console.log('Data inserted successfully');
    } catch (err) {
        console.error('Error inserting data:', err);
    } finally {
        // Закрытие соединения
        sql.close();
    }
}

// Запуск функции
insertDataToDB();
