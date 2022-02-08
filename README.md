# Squares API

Implementation description for junior homework task.


---
## Import a list of points

### Request

`POST /points/list`

	https://localhost:7126/points/list

### Response Body

	[
	    {
		"id": "65052012-9bd0-4c5d-98e4-76d1642620d0",
		"x": 0,
		"y": 0
	    },
	    {
		"id": "3b9449f5-2672-470f-9733-8a2a10de27a9",
		"x": 1,
		"y": 2
	    },
	    {
		"id": "2cf801d8-ee71-4d2f-ab5c-7f4b12c089fa",
		"x": 3,
		"y": 5
	    },
	    {
		"id": "cd853cf7-9b98-4bfa-afbc-6fb4191809e6",
		"x": 70,
		"y": 0
	    }
	]

---

## Add a point to an existing list

### Request

`GET /points`

	'Accept: application/json' -d  https://localhost:7126/points

    {
        "x": 70,
        "y": 0
    }

### Response Body

    Status: 200 OK

---

## Delete a point from existing list

### Request

`DEETE /points/{id}`

	https://localhost:7126/points/2cf801d8-ee71-4d2f-ab5c-7f4b12c089fa


### Response Body

    Status: 200 OK

---
