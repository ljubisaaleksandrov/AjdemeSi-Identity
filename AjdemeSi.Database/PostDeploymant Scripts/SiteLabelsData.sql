MERGE INTO SiteLabels AS TARGET USING (VALUES 
(1, N'en', N'From Place:', N'To Place:', N'Date', N'Time', N'Travel Time', N'Price Per Person', N'Price Per Ride', N'Start Delay', N'Break Time', N'Passengers No'),
(2, N'rs', N'Od:', N'Do', N'Datum', N'Vreme', N'Vreme Putovanja', N'Cena po osobi', N'Ukupna cena vožnje', N'Odložen polaza', N'Pauza', N'Broj putnika'),
(3, N'bg', N'От:', N'До', N'Дата', N'Време', N'Време на пътуване', N'Цена по човек', N'Обща цена', N'Забавено тръгване', N'Почивки', N'Брой на места')) 
AS Source (Id,
		   LanguageName, 
		   RideLocationFrom,
		   RideLocationTo,
		   RideStartDate,
		   RideStartTime,
		   RideTravelTIme,
		   RidePricePerPerson,
		   RidePricePerRide,
		   RideDelay,
		   RideBreakTime,
		   RidePassengers)
ON TARGET.Id = Source.Id
--	update matched rows 
WHEN MATCHED THEN 
UPDATE SET LanguageName = Source.LanguageName,
		   RideLocationFrom = Source.RideLocationFrom,
		   RideLocationTo = Source.RideLocationTo,
		   RideStartDate = Source.RideStartDate,
		   RideStartTime = Source.RideStartTime,
		   RideTravelTIme = Source.RideTravelTIme,
		   RidePricePerPerson = Source.RidePricePerPerson,
		   RidePricePerRide = Source.RidePricePerRide,
		   RideDelay = Source.RideDelay,
		   RideBreakTime = Source.RideBreakTime,
		   RidePassengers = Source.RidePassengers
	 
--	insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id,
		LanguageName, 
		RideLocationFrom,
		RideLocationTo,
		RideStartDate,
		RideStartTime,
		RideTravelTIme,
		RidePricePerPerson,
		RidePricePerRide,
		RideDelay,
		RideBreakTime,
		RidePassengers) 
VALUES (Id,
		LanguageName,
		RideLocationFrom,
		RideLocationTo,
		RideStartDate,
		RideStartTime,
		RideTravelTIme,
		RidePricePerPerson,
		RidePricePerRide,
		RideDelay,
		RideBreakTime,
		RidePassengers);