pokemongo-api application

aws s3api create-bucket --bucket pokemongo-api-builds --region eu-west-1 --create-bucket-configuration LocationConstraint=eu-west-1

dotnet lambda deploy-serverless

pokemongo-api-stack
pokemongo-api-builds

aws apigateway get-rest-apis
