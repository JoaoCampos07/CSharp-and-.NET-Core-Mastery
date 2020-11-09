
After installing the OpenSSL

1) How to create the certificates ? 

For the private key: openssl genpkey -algorithm RSA -out private_key.pem -pkeyopt rsa_keygen_bits:2048
genpkey specifying that we'll generate a private key;
-algorithm RSA the algorithm used, in this case RSA;
-out private_key.pem the output argument and path;
-pkeyopt rsa_keygen_bits:2048 set the public key algorithm and the key size;

For the public key: openssl rsa -pubout -in private_key.pem -out public_key.pem
rsa specifying that the command will process RSA keys;
-pubout -in private_key.pem the private key and the path of it;
-out public_key.pem the output argument and path;

2) How to convert the PEM Keys to a more frindly formate ? 
R: You can translate them to XML with :
https://superdry.apphb.com/tools/online-rsa-key-converter
