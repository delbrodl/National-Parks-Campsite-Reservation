DELETE FROM reservation;
DELETE FROM [site];
DELETE FROM campground;
DELETE FROM park;

INSERT INTO park VALUES ('New Park', 'Somewhere Over the Rainbow', '09/09/1988', 5000, 5000, 'This is a description');
DECLARE @parkId int = (SELECT @@Identity);

INSERT INTO campground VALUES ((SELECT park_id FROM park), 'Campground Name', 05, 09, 35.00)
DECLARE @campgroundId int = (SELECT @@Identity);

INSERT INTO site VALUES ((SELECT campground_id FROM campground), 1, 10, 0, 25, 0) 
DECLARE @siteId int = (SELECT @@Identity);

INSERT INTO reservation VALUES ((SELECT site_id FROM site), 'Site Name', '10/10/2017', '10/15/2017', '10/01/2017')
DECLARE @reservationId int = (SELECT @@Identity);

SELECT @parkId as parkId, @campgroundId as campgroundId, @siteId as siteId, @reservationId as reservationId