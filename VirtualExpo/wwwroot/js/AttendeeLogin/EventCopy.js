function CopyLinkToExhibition(id) {
    navigator.clipboard.writeText("/Attendee/ExhibitionHome/"+id);

    alert("Copied the text: " + "/Attendee/ExhibitionHome/" + id);
}