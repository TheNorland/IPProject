// api/index.js
const fs = require('fs');
const path = require('path');

module.exports = async (req, res) => {
  try {
    // Get the IP address from the request headers
    const ipAddress = req.headers['x-real-ip'] || req.connection.remoteAddress;

    // Display a "Thanks!" message
    res.status(200).send('Thanks!');

    // Log the IP address to a file
    const logFilePath = path.join(__dirname, 'ip_logs.txt');
    const logEntry = `${new Date().toISOString()} - ${ipAddress}\n`;

    fs.appendFileSync(logFilePath, logEntry, 'utf-8');
  } catch (error) {
    console.error('Error processing request:', error);
    res.status(500).send('Internal Server Error');
  }
};
