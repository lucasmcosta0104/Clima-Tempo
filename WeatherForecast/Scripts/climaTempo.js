var tempos = [
    { tempo: 'ensolarado', link: 'https://i.pinimg.com/originals/2e/fe/df/2efedf801be886dc03da5714b4766a2a.png' },
    { tempo: 'nublado', link: 'https://creazilla-store.fra1.digitaloceanspaces.com/emojis/47934/sun-behind-large-cloud-emoji-clipart-md.png' },
    { tempo: 'chuvoso', link: 'https://1.bp.blogspot.com/-EZP-8JSoYfs/YTYodsz5nfI/AAAAAAAAt3g/21T-2NX2zAQlen8lV9NfLl6wM5QIzpoJwCLcBGAsYHQ/s1800/rainbow%2Bclipart-03.png' },
    { tempo: 'tempestuoso', link: 'https://creazilla-store.fra1.digitaloceanspaces.com/emojis/57058/cloud-with-lightning-and-rain-emoji-clipart-md.png' },
    { tempo: 'instavel', link: 'https://gfx.nrk.no/TAkOWAxDvw7QuOFJ3I2LDw19dKcjQuEn5LO94EZoheYQ.png' },

]

function showClimaTempo(id) {
    $.getJSON("PrevisaoClima/GetPrevisao/" + id,
        function (data) {
            var i = 1;
            $.each(data, function (key, item) {
                $('#diaMes' + i).html(item.DiaMes);
                $('#dia' + i).html(item.Dia);
                $('#clima' + i).html(item.Clima);
                $('#maxima' + i).html('Máxima: ' + item.TemperaturaMaxima + 'C°');
                $('#minima' + i).html('Mínima: ' + item.TemperaturaMinima + 'C°');

                //var img = document.createElement("IMG");
                //img.src = tempos.find(x => x.tempo == item.Clima).link;
                //img.style.width = "150px";
                //img.style.height = "150px";
                document.getElementById('card' + i).style.backgroundImage = 'url(' + tempos.find(x => x.tempo == item.Clima).link + ')';
                document.getElementById('card' + i).style.backgroundSize = '100%';
                i++;
            });
            document.getElementById('dia').style.display = '';
            $('#cidade').html('Clima para os próximos sete dias na cidade de ' + data[0].Cidade);
        });
}
