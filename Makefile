modeltests:
	@echo Build Docker image:
	docker build -f Dockerfiles/ModelTests.dockerfile -t herokuapp/modeltests .

	@echo Run Docker container:
	docker run --rm herokuapp/modeltests

webapi:
	@echo Build Docker image:
	docker build -f Dockerfiles/WebAPI.dockerfile -t herokuapp/webapi .

	@echo Run docker container:
	docker run -d --rm -p 5000:80 herokuapp/webapi

webapi.heroku:
	@echo Login to Heroku:
	echo ${HEROKU_API_KEY} | docker login -u=_ --password-stdin registry.heroku.com
	
	@echo Build Docker image:
	docker build -f Dockerfiles/WebAPI.heroku.dockerfile -t registry.heroku.com/whippingboy/web .

	@echo Push image:
	docker push registry.heroku.com/whippingboy/web

	@echo Release container:
	heroku container:release web -a whippingboy