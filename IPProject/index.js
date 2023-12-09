// index.js

const express = require('express');
const bodyParser = require('body-parser');

const app = express();

// Middleware to parse JSON in the request body
app.use(bodyParser.json());

// Endpoint to receive the IP address
app.post('/receive-ip', (req, res) => {
    const ipAddress = req.body.ipAddress;

    console.log(`Received IP Address: ${ipAddress}`);

    // You can do further processing with the IP address here

    res.status(200).json({ message: 'IP address received successfully' });
});

// Start the server
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
