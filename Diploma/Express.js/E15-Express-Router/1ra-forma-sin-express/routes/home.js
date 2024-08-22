function HomeRoutes(app){
    app.get('/about', function(req, res){
        res.send('about page');
    });

    app.get("/dashboard", (req, res) => {
        res.sendFile("Dashboard page");
    });
}

module.exports = HomeRoutes;