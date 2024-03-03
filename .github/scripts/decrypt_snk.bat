SET sn="C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\sn.exe"

echo "Decrypting key..."
gpg --quiet --batch --yes --passphrase %SNK_PASSPHRASE% --output Winnster.snk -d ./.github/secrets/Winnster.snk.gpg

echo "Extracting public key..."
%sn% -p Winnster.snk Winnster.PublicKey
%sn% -tp Winnster.PublicKey