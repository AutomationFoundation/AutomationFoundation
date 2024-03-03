echo "Decrypting key..."
gpg --quiet --batch --yes --passphrase %SNK_PASSPHRASE% --output Winnster.snk -d ./.github/secrets/Winnster.snk.gpg

echo "Extracting public key..."
sn -p Winnster.snk Winnster.PublicKey
sn -tp Winnster.PublicKey