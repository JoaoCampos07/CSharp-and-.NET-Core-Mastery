
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

3) What is a PEM key ?

4) What is the PEM format ?
.pem - Defined in RFCs 1421, this is a container format that may include just the public certificate
or may include an entire certificate chain including public key, private key, and root certificates. 
Confusingly, it may also encode a CSR (e.g. as used here) as the PKCS10 format can be translated into PEM.
The name is from Privacy Enhanced Mail (PEM), a failed method for secure email but the container format it used lives on,
and is a base64 translation of the x509 ASN.1 keys.

5) What is a p12 format ? 
Originally defined by RSA in the Public-Key Cryptography Standards (abbreviated PKCS), 
the "12" variant was originally enhanced by Microsoft, and later submitted as RFC 7292. 
This is a passworded container format that contains both public and private certificate pairs. 
Unlike .pem files, this container is fully encrypted. 
Openssl can turn this into a .pem file with both public and private keys: openssl pkcs12 -in file-to-convert.p12 -out converted-file.pem -nodes