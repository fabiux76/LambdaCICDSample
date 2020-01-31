# Modalità di deploy

```
aws cloudformation package --template-file .\LambdaDynamoCICD\serverless.template --output-template-file serverless-output.json --use-json --s3-bucket my-first-function-frm-vsk-serverless
```
```
aws cloudformation deploy --template-file c:\Projects\AWS\LambdaDynamoCICD\serverless-output.json --stack-name StudentAppStack
```

Quindi aggiungere manualmente un utente con id=1 alla tabella

# Workaround per le cose che non tornano
- In Integration Request devo andare a cambiare la funzione, impostando quella creata ex novo

# Cose che non mi tornano:
- Ho dovuto attivare proxy integration su api gateway di modo che funzionasse
- Avrei dovuto scrivere nel file swagger l'arn della funzione, ma prima che venisse creata con il cloudformation.... Questo è evidentemente un giro che non funziona
	per farlo funzionare ho dovuto definire il studentswagger con riferimento ad un'altra lambda, e poi lo modifico dopo manualmente
- il test event in lambda, conformemente a proxy integration, ha questo formato:
```
{
  "queryStringParameters": {
    "studentid": "1"
  }
}
```