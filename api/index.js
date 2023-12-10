// api/index.js
const fs = require('fs');
const path = require('path');

module.exports = async (req, res) => {
  try {
    const logFilePath = path.join(__dirname, 'ip_logs.txt');
    const data = fs.readFileSync(logFilePath, 'utf-8');
    const ipAddresses = data.split('\n').filter(Boolean);
    res.json({ ipAddresses });
  } catch (error) {
    console.error('Error reading IP logs:', error);
    res.status(500).json({ error: 'Internal Server Error' });
  }
};
