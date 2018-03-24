INSERT INTO Category (Id, Name, ColourIndex) VALUES ('125D38D8-B2C7-479B-A270-7D2B73FF4055', N'Test Category 1', 0)
INSERT INTO Category (Id, Name, ColourIndex) VALUES ('F24B1469-B85C-43B2-9B4D-6C79A95A95D3', N'Test Category 2', 0)
INSERT INTO Category (Id, Name, ColourIndex) VALUES ('0EAFA4B7-7793-428F-95F8-84F6E0F93FCA', N'Test Category 3', 0)

INSERT INTO Roadmap (Id, Name, Description) VALUES ('DDE54DD1-3ADC-4603-9F7E-5ABB4D23A119', N'First technical roadmap', 'blah about first tech roadmap')
INSERT INTO Roadmap (Id, Name, Description) VALUES ('4237322D-6C82-438D-864B-8689B437CC2D', N'Second technical roadmap', 'blah about second tech roadmap')
INSERT INTO Roadmap (Id, Name, Description) VALUES ('BDABF7DF-00E0-4E65-A227-9806690A501C', N'Third technical roadmap', 'blah about third tech roadmap')

INSERT INTO Swimlane (Id, RoadmapId, Name, SortOrder) VALUES ('3FDC05D9-B01E-42B0-B2C7-BAE928D3C80A', 'DDE54DD1-3ADC-4603-9F7E-5ABB4D23A119', N'1st Swimlane 1', 0)
INSERT INTO Swimlane (Id, RoadmapId, Name, SortOrder) VALUES ('C66509B8-8005-4DA6-B3F0-B36329B8E4A4', 'DDE54DD1-3ADC-4603-9F7E-5ABB4D23A119', N'1st Swimlane 2', 100)
INSERT INTO Swimlane (Id, RoadmapId, Name, SortOrder) VALUES ('5B3E587E-65C9-4B0D-B14B-17FE43946D34', 'DDE54DD1-3ADC-4603-9F7E-5ABB4D23A119', N'1st Swimlane 3', 200)

INSERT INTO Swimlane (Id, RoadmapId, Name, SortOrder) VALUES ('2F8F76A1-C8CC-4AD1-B9ED-DB0933725035', '4237322D-6C82-438D-864B-8689B437CC2D', N'2nd Swimlane 1', 0)
INSERT INTO Swimlane (Id, RoadmapId, Name, SortOrder) VALUES ('431841E2-F1C7-41B0-85D3-DC4A660E6D2B', '4237322D-6C82-438D-864B-8689B437CC2D', N'2nd Swimlane 2', 100)
INSERT INTO Swimlane (Id, RoadmapId, Name, SortOrder) VALUES ('0A5E4E9D-EF83-4C7A-A4CF-41534A66186F', '4237322D-6C82-438D-864B-8689B437CC2D', N'2nd Swimlane 3', 200)
