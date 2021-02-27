WEBAPI_IMAGE_NAME = gira/webapi
HEROKU_APP_NAME = girabot

webapi:
	@echo Build Docker image:
	docker build -f Dockerfiles/WebAPI.dockerfile -t $(WEBAPI_IMAGE_NAME) .

	@echo Run docker container:
	docker run -d --rm -p 5000:80 $(WEBAPI_IMAGE_NAME)

webapi.heroku:
	@echo Login to Heroku:
	echo ${HEROKU_API_KEY} | docker login -u=_ --password-stdin registry.heroku.com
	
	@echo Build Docker image:
	docker build -f Dockerfiles/WebAPI.heroku.dockerfile -t registry.heroku.com/$(HEROKU_APP_NAME)/web .

	@echo Push image:
	docker push registry.heroku.com/$(HEROKU_APP_NAME)/web

	@echo Release container:
	heroku container:release web -a $(HEROKU_APP_NAME)