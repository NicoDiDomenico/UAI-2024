USE MindFitMaster;
GO

INSERT INTO Gym (NombreGym, ConnectionString, Activo, FechaCreacion)
VALUES 
(
    'MindFit Intelligence HQ (Entorno Dev)', 
    'Server=DiDomenicoPC\SQLEXPRESS; Database=MindFitIntelligence; User Id=nicolas_didomenico; Password=0045981746; TrustServerCertificate=True;', 
    1, 
    GETUTCDATE()
),
(
    'Saries Gym (Cliente Demo)', 
    'Server=DiDomenicoPC\SQLEXPRESS; Database=MindFit_ClienteDemo; User Id=nicolas_didomenico; Password=0045981746; TrustServerCertificate=True;', 
    1, 
    GETUTCDATE()
);
GO